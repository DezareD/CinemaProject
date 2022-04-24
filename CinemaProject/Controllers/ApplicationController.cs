using CinemaProject.Data;
using CinemaProject.Data.JsonData;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace CinemaProject.Controllers
{
    [Route("api/[action]")]
    public class ApplicationController : Controller
    {
        public ApplicationController(ApplicationContext db)
        {
            this.db = db;
        }

        public ApplicationContext db { get; set; }

        [HttpGet("/")]
        public async Task<IActionResult> Index()
        {
            // Модель представления
            CinamePackage returnModel = new CinamePackage();

            // HTTP клиент для работы с API
            using var httpClient = new HttpClient();


            // Выставляем заголовки
            httpClient.DefaultRequestHeaders.Add("accept", "application/json");
            httpClient.DefaultRequestHeaders.Add("X-API-KEY", "a87f294f-5239-4280-9b05-85186bca4201");

            // Работа с рандомом
            var rnd = new Random();

            // Получаем JSON ответ и парсим его в модель
            var json = await httpClient.GetStringAsync("https://kinopoiskapiunofficial.tech/api/v2.2/films?order=RATING&type=FILM&ratingFrom=0&ratingTo=10&yearFrom=2000&yearTo=2022&page=" + rnd.Next(1, 20));
            returnModel = JsonConvert.DeserializeObject<CinamePackage>(json);

            // Получаем JSON ответ и парсим его в модель
            var newesJson = await httpClient.GetStringAsync("https://kinopoiskapiunofficial.tech/api/v2.2/films/top?type=TOP_AWAIT_FILMS&page=1");
            var jsonNewest = JsonConvert.DeserializeObject<CinemaTops>(newesJson);

            returnModel.lastCinemas = jsonNewest.films;

            // Возвращаем готовую модель в представление
            return View("Index", returnModel);
        }

        [HttpGet]
        public async Task<IActionResult> Shedule(int id)
        {
            // HTTP клиент для работы с API
            using var httpClient = new HttpClient();

            // Выставляем заголовки
            httpClient.DefaultRequestHeaders.Add("accept", "application/json");
            httpClient.DefaultRequestHeaders.Add("X-API-KEY", "a87f294f-5239-4280-9b05-85186bca4201");

            // Работа с рандомом
            var rnd = new Random();

            // Получаем JSON ответ и парсим его в модель
            var json = await httpClient.GetStringAsync("https://kinopoiskapiunofficial.tech/api/v2.2/films/" + id);

            var cinema = JsonConvert.DeserializeObject<Cinema>(json);

            var model = new CinemaShedule(cinema);

            // Возвращаем готовую модель в представление
            return View(model);
        }

        /* ВХОД */

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginModel model)
        {
            // проверяем модель на валидность
            if (ModelState.IsValid)
            {
                // ищем пользователя с таким никнеймом
                var find = db.users.Where(m => m.Name == model.Name).FirstOrDefault();

                if(find == null) // если такого пользователя нет
                {
                    ModelState.AddModelError("", "Неверный ввод");
                    return View();
                }

                if(find.Password != model.Password) // если пароль не совпадает
                {
                    ModelState.AddModelError("", "Неверный ввод");
                    return View();
                }

                // авторизируем пользователя
                await Authenticate(find);

                // перенаправляем на основную странциу
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Неверный ввод");
                return View();
            }
        }

        /* регистрация и выход */

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Contacts()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public IActionResult Logout()
        {
            // удалаяем куки авторизации у пользователя
            HttpContext.SignOutAsync();
            return RedirectToAction("Index");
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            // проверяем модель на валидность
            if (ModelState.IsValid)
            {
                // ищем пользователя с таким же именем
                var find = db.users.Where(m => m.Name == model.Name).FirstOrDefault();

                if (find != null) // если такой есть - выдаем ошибку
                {
                    ModelState.AddModelError("", "Пользователь с таким именем уже существует!");
                    return View();
                }

                // модель пользователя
                var user = new User()
                {
                    Email = model.Email,
                    Name = model.Name,
                    Password = model.Password
                };

                // загоням пользователя
                db.users.Add(user);
                await db.SaveChangesAsync();
            
                // авторизируем
                await Authenticate(user);

                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Неверный ввод");
                return View();
            }
        }

        private async Task Authenticate(User user)
        {
            // работаем с куки, добавляем новые куки пользователю
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Name)
            };

            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}

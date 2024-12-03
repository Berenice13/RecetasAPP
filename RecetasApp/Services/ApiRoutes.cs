namespace RecetasApp.ApiRoutes;

public static class ApiRoutes
{
    public const string BaseUrl = "http://192.168.233.70:3333";

    public static class Routes
    {
        public const string Login = "/login";
        public const string Register = "/register";
        public const string UserInfo = "/authUser";
        public const string UserUpdate = "/update";
        public const string RecetasIndex = "/recetas";
        public const string RecetasUser = "/recetas/user";
        public const string RecetasTop = "/recetas/top";
        public const string RecetasFavoritas = "/recetas/liked";
    }
}

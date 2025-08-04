namespace EcommerceBackend.UtilityClasses
{
    public static class ValidationStatus
    {
        public static bool Success = true;
        public static bool Fail = false;



        public enum ErrorTypes { BadRequest,ServerError };
    }
}

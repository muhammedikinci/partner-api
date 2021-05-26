using System;

namespace Application.AppException
{
    public class ExceptionConstants
    {
        public static readonly string PASSWORD_REQUIRED = "Şifre alanı zorunludur.";
        public static readonly string PASSWORD_WHITESPACES = "Şifre boşluktan oluşamaz.";
        public static readonly string USER_IS_NOT_VALID = "Kullanıcı geçerli değil.";
        public static readonly string USER_CLAIM_NOT_VALID = "Gerekli kullanıcı verileri sağlanmadı.";
        public static readonly string ORDER_NOT_FOUND = "Sipariş bulunamadı.";
        public static readonly string NOT_FOUND = "İstenilen öğe bulunamadı.";
        public static readonly string PERMISSON_DENIED = "Yetkisiz erişim.";
        public static readonly string USER_BLOCKED = "Çok fazla başarısız deneme gerçekleştirildi. Lütfen daha sonra tekrar deneyiniz.";
    }
}
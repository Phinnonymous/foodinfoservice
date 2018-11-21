using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodInfo.Service.Helper
{
    public class PublicConstants
    {
        #region Errors

        #region Login

        public static string UserNameOrEmailAlreadyExistError { get; set; } = "LGN001";
        public static string PasswordRequired { get; set; } = "LGN002";
        public static string UsernameOrPasswordWrong { get; set; } = "LGN003";
        #endregion

        #region System
        public static string SysErrorMessage { get; set; } = "SYS001";
        #endregion
        #region User
        public static string UserNotFoundError { get; set; } = "USR001";
        public static string ModifiedUserIdRequired { get; set; } = "USR002";

        #endregion
        #region Language
        public static string NoLanguageFound { get; set; } = "LNG001";

        #endregion

        #endregion
    }
}

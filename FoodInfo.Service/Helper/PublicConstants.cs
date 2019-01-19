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
        public static string ProivdeModifiedUserId { get; set; } = "SYS002";
        public static string ProvideACreatedUserId { get; set; } = "SYS003";
        #endregion
        #region User
        public static string UserNotFoundError { get; set; } = "USR001";
        public static string ModifiedUserIdRequired { get; set; } = "USR002";

        #endregion
        #region Language
        public static string NoLanguageFound { get; set; } = "LNG001";

        #endregion
        #region Category
        public static string NoCategoryFound { get; set; } = "CTG0001";
        public static string ProvideCategoryName{ get; set; } = "CTG0002";
        public static string AlreadyACategoryDefinedWithThisName { get; set; } = "CTG0003";
        public static string ProvideLanguageCode { get; set; } = "CTG0004";

        #endregion
        #region Content
        public static string BarcodIdRequired { get; set; } = "CNT0001";
        public static string ExistingContentForProduct { get; set; } = "CTN0002";
        public static string BarcodeIdOrLanguageCodeDoesNotFound { get; set; } = "CTN0003";
        public static string ProvideAtLeastOneImage { get; set; } = "CTN0004";
        public static string ProductNotFound { get; set; } = "CTN0005";
        public static string ProductContentIdRequired { get; set; } = "CTN0006";
        #endregion
        #region Comment
        public static string CommentCanNotBeEmpty { get; set; } = "CMT0001";
        public static string CommentNotFound { get; set; } = "CMT0002";
        #endregion
        #region Vote
        public static string UserAlreadyVoteThisProduct { get; set; } = "VTE0001";
        #endregion
        #region Product
        public static string ProductDoesNotFound { get; set; } = "PRD0001";
        public static string ProductHasAlreadyGroup { get; set; } = "PRD0002";
        public static string PleaseSelectMoreThanOneProduct { get; set; } = "PRD0003"; 
        #endregion
        #endregion
    }
}

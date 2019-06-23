using System;
using Enums;

namespace Utils {
    public class RequestMethodTypeUtils {
        public static String getRequestMethodTypeString(RequestMethodTypeEnum enumType) {
            String type = "";
            switch (enumType) {
                case RequestMethodTypeEnum.Get:
                    type = "GET";
                    break;

                case RequestMethodTypeEnum.Delete:
                    type = "DELETE";
                    break;

                case RequestMethodTypeEnum.Put:
                    type = "PUT";
                    break;

                default:
                    type = "POST";
                    break;
            }

            return type;
        }
    }
}
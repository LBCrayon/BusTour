using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace DataAccess.Helpers
{
	public class ErrorEnum
	{
        public enum BusErrorEnums
        {
            //404
            [Display(Name = "Not found this bus!")]
            NOT_FOUND = 4041,

            [Display(Name = "Not found this bus code!")]
            NOT_FOUND_CODE = 4042,

            //400
            [Display(Name = "This bus code already exsist!")]
            PRODUCT_CODE_EXSIST = 4001,
        }

        public enum JourneyErrorEnums
        {
            //404
            [Display(Name = "Not found this Journey!")]
            NOT_FOUND = 4041,

            [Display(Name = "Not found this Journey code!")]
            NOT_FOUND_CODE = 4042,

            //400
            [Display(Name = "This bus Journey already exsist!")]
            PRODUCT_CODE_EXSIST = 4001,
        }
        public enum ClassErrorEnums
        {
            //404
            [Display(Name = "Not found this Class!")]
            NOT_FOUND = 4041,

            [Display(Name = "Not found this Class code!")]
            NOT_FOUND_CODE = 4042,

            //400
            [Display(Name = "This bus Class already exsist!")]
            PRODUCT_CODE_EXSIST = 4001,
        }
    }
}


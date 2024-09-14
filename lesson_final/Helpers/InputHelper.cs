using System.Data;

namespace lesson_final.Data;

public class InputHelper
    {
        private static string _subMessage;

        static InputHelper()
        {
            _subMessage = "Enter";
        }
        /// <summary>
        /// Enter: Any integer value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int GetInt(string value)
        {
            int result = 0;
            Console.Write("{0} {1}: ", _subMessage, value);
            while (!int.TryParse(Console.ReadLine(), out result))
            {
                Console.Write("{0} {1}: ", _subMessage, value);
            }
            return result;
        }
        /// <summary>
        /// Enter: Cash equivalent
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double GetDouble(string value)
        {
            double result = 0;
            Console.Write("{0} {1}: ", _subMessage, value);
            while (!double.TryParse(Console.ReadLine(), out result))
            {
                Console.Write("{0} {1}: ", _subMessage, value);
            }
            return result;
        }

        public static double GetPositiveDouble(string value)
        {
            double result = 0;
            do
            {
                result = GetDouble(value);
                if (result > 0) break;
                value = "number greater than 0";
            }
            while (true);

            return result;
        }
        /// <summary>
        /// Enter: True or False
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool GetBoolean(string value)
        {
            bool result = false;
            Console.Write("{0} {1}: ", _subMessage, value);
            while (!bool.TryParse(Console.ReadLine(), out result))
            {
                Console.Write("{0} {1}: ", _subMessage, value);
            }
            return result;
        }
        /// <summary>
        /// Enter: Not empty string
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetString(string value)
        {
            string result = String.Empty;
            Console.Write("{0} {1}: ", _subMessage, value);
            while (String.IsNullOrWhiteSpace(result = Console.ReadLine()))
            {
                Console.Write("{0} {1}: ", _subMessage, value);
            }
            return result;
        }
        /// <summary>
        /// Enter: Valid date
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateOnly GetDate(string value)
        {
            DateOnly dateOnly = new();
            Console.Write("{0} {1}: ", _subMessage, value);
            while (!DateOnly.TryParse(Console.ReadLine(), out dateOnly))
            {
                Console.Write("{0} {1}: ", _subMessage, value);
            }
            return dateOnly;
        }
    }

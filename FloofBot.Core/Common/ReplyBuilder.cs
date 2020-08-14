using System.Collections.Generic;

namespace FloofBot.Core.Common
{
    public class ReplyBuilder
    {
        private Dictionary<string, object> _variables = new Dictionary<string, object>();

        public ReplyBuilder AddVariable(string key, object data)
        {
            _variables.Add(key, data);
            return this;
        }

        public string GetString(string input)
        {
            string output = input;
            
            foreach (KeyValuePair<string, object> variable in _variables)
            {
                output = input.Replace($"%{variable.Key}%", variable.Value.ToString());
            }

            return output;
        }
    }
}
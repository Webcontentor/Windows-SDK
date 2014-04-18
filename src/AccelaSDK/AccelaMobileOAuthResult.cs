﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accela.WindowsStoreSDK
{
    /// <summary>
    /// 
    /// </summary>
    public class AccelaOAuthResult : AccelaOAuthErrorResult
    {

        /// <summary>
        /// The authorization code generated by the authorization server.
        /// </summary>
        public String Code { get; private set; }

        public String Environment { get; private set; }

        public String Agency { get; private set; }

        internal AccelaOAuthResult(IDictionary<string, object> parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException("parameters");
            }

            if (parameters.ContainsKey("state"))
            {
                base.state = parameters["state"].ToString();
            }

            if (parameters.ContainsKey("error"))
            {
                base.error = parameters["error"].ToString();

                if (parameters.ContainsKey("description"))
                {
                    base.error_description = parameters["description"].ToString();
                }
            }
            else
            {
                if (parameters.ContainsKey("code"))
                {
                    this.Code = parameters["code"].ToString();
                }
                if (parameters.ContainsKey("environment"))
                {
                    this.Environment = parameters["environment"].ToString();
                }
                if (parameters.ContainsKey("agency_name"))
                {
                    this.Agency = parameters["agency_name"].ToString();
                }
            }
        }

        /// <summary>
        /// Converts values of this instance to a System.String.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var strBuilder = new StringBuilder();

            strBuilder.AppendLine("Code: " + this.Code);
            strBuilder.AppendLine("State: " + base.state);
            strBuilder.AppendLine("Environment: " + this.Environment);
            strBuilder.AppendLine("Error: " + base.error);
            strBuilder.AppendLine("Error_Description: " + base.error_description);
            strBuilder.AppendLine("Error_Uri: " + base.error_uri);
            return strBuilder.ToString();
        }
    }
}

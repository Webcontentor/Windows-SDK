﻿using RecordsViewer.Portable;
using RecordsViewer.Portable.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accela.WindowsStoreSDK;
using Newtonsoft.Json;

namespace RecordsViewer.ViewModels
{
    public class RecordService : IRecordService
    {
        public Task<WSRecord> GetRecordAsync(string recordId)
        {
            return Task.Factory.StartNew(() =>
            {
                return App.RecordsViewModel.Records.FirstOrDefault(c => c.Id == recordId);
            });
        }

        public Task<List<WSRecord>> GetRecordsAsync(string servicePath, IDictionary<string, object> @params)
        {
            return Task.Run(() =>
            {
                var response = App.SharedSDK.GetAsync(servicePath, @params).Result;
                var result = JsonConvert.DeserializeObject<WSRecordsResponse>(response.ToString());
                return result.WSRecords;
            });
        }


        public Task<WSRecordLocation> GetRecordLocation(string recordId)
        {
            return Task.Run(() =>
            {
                WSRecordLocation location = null;
                try
                {
                    return App.SharedSDK.GetAsync<WSRecordLocation>(String.Format("/v4/records/{0}/location", recordId)).Result;
                }
                catch (AggregateException ex)
                {
                    if (ex.InnerException != null)
                        throw ex.InnerException;
                }
                return location;
            });
        }
    }
}

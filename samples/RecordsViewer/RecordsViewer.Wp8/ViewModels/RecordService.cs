﻿using Accela.WindowsStoreSDK;
using Newtonsoft.Json;
using RecordsViewer.Entities;
using RecordsViewer.Portable;
using RecordsViewer.Portable.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordsViewer.ViewModels
{   /// <summary>
    /// Provide get a specific Record or Records list function.
    /// </summary>
    public class RecordService : IRecordService
    {
        public Task<WSRecord> GetRecordAsync(string recordId)
        {
            return Task.Factory.StartNew(() =>
            {
                return App.RecordsViewModel.Records.FirstOrDefault(c => c.Id == recordId);
            });
        }

        public async Task<List<WSRecord>> GetRecordsAsync(string servicePath, IDictionary<string, object> @params)
        {
            List<WSRecord> records = null;
            try
            {
                RecordSearchCondition condition = new RecordSearchCondition();
                condition.RecordId = "14CAP-00000-000HX";
                string jsonString = JsonConvert.SerializeObject(condition);
                var response = await App.SharedSDK.PostAsync(servicePath, jsonString);

                var result = JsonConvert.DeserializeObject<WSRecordsResponse>(response.ToString());
                records = result.WSRecords;
            }
            catch (AggregateException ex)
            {
                if (ex.InnerException != null)
                    throw ex.InnerException;
            }

            return records;
        }
    }
}

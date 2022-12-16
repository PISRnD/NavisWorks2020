﻿using Amazon.KinesisFirehose;
using Amazon.KinesisFirehose.Model;
using PivdcSupportModel;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;

namespace PivdcSupportModule
{
    public class OnlineUsage
    {
        public AmazonKinesisFirehoseClient amazonKinesisFirehoseClient { get; set; }
        string AccessKeyId = "AKIA2OIU4PNUVJU67B4V";
        string SecretAccessKey = "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";

        public OnlineUsage()
        {
            amazonKinesisFirehoseClient = new AmazonKinesisFirehoseClient(AccessKeyId, SecretAccessKey, Amazon.RegionEndpoint.APSouth1);
        }

        public bool InsertUsage(UsageDetail usageDetail)
        {
            byte[] oByte = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(usageDetail));
            try
            {
                using (MemoryStream ms = new MemoryStream(oByte))
                {
                    PutRecordRequest requestRecord = new PutRecordRequest
                    {
                        DeliveryStreamName = "pi_usage_data",
                        Record = new Record
                        {
                            Data = ms
                        }
                    };
                    PutRecordResponse response = amazonKinesisFirehoseClient.PutRecord(requestRecord);
                    if (response.HttpStatusCode == HttpStatusCode.OK)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ErrorLogEntry(MethodBase.GetCurrentMethod().Name);
            }
            return false;
        }
    }
}
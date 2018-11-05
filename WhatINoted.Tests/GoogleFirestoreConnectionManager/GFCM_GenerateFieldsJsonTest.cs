using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using WhatINoted.ConnectionManagers;
using static WhatINoted.ConnectionManagers.GoogleFirestoreConnectionManager;

namespace WhatINoted.Tests.GoogleFirestoreConnectionManagerTests
{
    class GFCM_GenerateFieldsJsonTest : GFCM_Test
    {
        public override bool Run(StreamWriter sw)
        {
            bool passed = true;
            passed = NoConfigs(sw) && passed;
            passed = OneConfig(sw) && passed;
            passed = MultipleConfigs(sw) && passed;
            passed = ConfigsListIsNull(sw) && passed;
            passed = ConfigItem1ValueIsNull(sw) && passed;
            passed = ConfigItem3ValueIsNull(sw) && passed;
            passed = TestWritingToDatabaseUsingAllFieldTypes(sw) && passed;
            return passed;
        }

        public bool NoConfigs(StreamWriter sw)
        {
            string expected = "{\"fields\":{}}";

            List<Tuple<string, FieldTypes, string>> fieldConfigList = new List<Tuple<string, FieldTypes, string>>();

            string actual = GenerateFieldsJson(fieldConfigList);

            return actual == expected;
        }

        public bool OneConfig(StreamWriter sw)
        {
            string expected = "{\"fields\":{\"fieldName1\":{\"stringValue\":\"fieldValue1\"}}}";

            List<Tuple<string, FieldTypes, string>> fieldConfigList = new List<Tuple<string, FieldTypes, string>>
            {
                new Tuple<string, FieldTypes, string>("fieldName1", FieldTypes.StringValue, "fieldValue1")
            };

            string actual = GenerateFieldsJson(fieldConfigList);

            return actual == expected;
        }

        public bool MultipleConfigs(StreamWriter sw)
        {
            string expected = "{\"fields\":{\"fieldName1\":{\"stringValue\":\"fieldValue1\"},\"fieldName2\":{\"integerValue\":\"3\"},\"fieldName3\":{\"doubleValue\":\"3.14\"}}}";

            List<Tuple<string, FieldTypes, string>> fieldConfigList = new List<Tuple<string, FieldTypes, string>>
            {
                new Tuple<string, FieldTypes, string>("fieldName1", FieldTypes.StringValue, "fieldValue1"),
                new Tuple<string, FieldTypes, string>("fieldName2", FieldTypes.IntegerValue, "3"),
                new Tuple<string, FieldTypes, string>("fieldName3", FieldTypes.DoubleValue, "3.14")
            };

            string actual = GenerateFieldsJson(fieldConfigList);

            return actual == expected;
        }

        public bool ConfigsListIsNull(StreamWriter sw)
        {
            try
            {
                GenerateFieldsJson(null);
            }
            catch (ArgumentNullException)
            {
                return true;
            }
            return false;
        }

        public bool ConfigItem1ValueIsNull(StreamWriter sw)
        {
            List<Tuple<string, FieldTypes, string>> fieldConfigList = new List<Tuple<string, FieldTypes, string>>
            {
                new Tuple<string, FieldTypes, string>("fieldName1", FieldTypes.StringValue, "fieldValue1"),
                new Tuple<string, FieldTypes, string>(null, FieldTypes.StringValue, "fieldValue2")
            };

            try
            {
                GenerateFieldsJson(fieldConfigList);
            }
            catch (ArgumentException)
            {
                return true;
            }
            return false;
        }

        public bool ConfigItem3ValueIsNull(StreamWriter sw)
        {
            List<Tuple<string, FieldTypes, string>> fieldConfigList = new List<Tuple<string, FieldTypes, string>>
            {
                new Tuple<string, FieldTypes, string>("fieldName1", FieldTypes.StringValue, "fieldValue1"),
                new Tuple<string, FieldTypes, string>("fieldName2", FieldTypes.StringValue, null)
            };

            try
            {
                GenerateFieldsJson(fieldConfigList);
            }
            catch (ArgumentException)
            {
                return true;
            }
            return false;
        }

        public bool TestWritingToDatabaseUsingAllFieldTypes(StreamWriter sw)
        {
            DateTime now = DateTime.UtcNow;
            JsonTest expected = new JsonTest()
            {
                fields = new JsonTestFields()
                {
                    fieldName1 = new JsonStringField(),
                    fieldName2 = new JsonNumericField(),
                    fieldName3 = new JsonNumericField(),
                    fieldName4 = new JsonDateTimeField()
                },
                StringValue = "fieldValue1",
                IntegerValue = 3,
                DoubleValue = 3.14,
                TimestampValue = now
            };
            
            List<Tuple<string, FieldTypes, string>> fieldConfigList = new List<Tuple<string, FieldTypes, string>>
            {
                new Tuple<string, FieldTypes, string>("fieldName1", FieldTypes.StringValue, "fieldValue1"),
                new Tuple<string, FieldTypes, string>("fieldName2", FieldTypes.IntegerValue, "3"),
                new Tuple<string, FieldTypes, string>("fieldName3", FieldTypes.DoubleValue, "3.14"),
                new Tuple<string, FieldTypes, string>("fieldName4", FieldTypes.TimestampValue, now.ToString("o"))
            };

            string json = GenerateFieldsJson(fieldConfigList);

            WebClient webClient = new WebClient();
            webClient.BaseAddress = "https://firestore.googleapis.com/v1beta1/projects/whatinoted-12345/databases/(default)/documents/test_collection/";
            try
            {
                // ensure document doesn't already exist
                webClient.UploadString("test_document", "DELETE", "");

                string result = webClient.UploadString("?documentId=test_document", json);

                // remove document after test
                webClient.UploadString("test_document", "DELETE", "");

                JsonTest actual = JsonConvert.DeserializeObject<JsonTest>(result);

                return actual.Equals(expected);
            }
            catch
            {
                return false;
            }
        }

        private class JsonTest : JsonEntity

        {
            public JsonTestFields fields { get; set; }

            public string StringValue
            {
                get
                {
                    return fields.fieldName1.stringValue;
                }

                set
                {
                    fields.fieldName1.stringValue = value;
                }
            }
            public int IntegerValue
            {
                get
                {
                    return fields.fieldName2.IntegerValue;
                }

                set
                {
                    fields.fieldName2.IntegerValue = value;
                }
            }
            public double DoubleValue
            {
                get
                {
                    return fields.fieldName3.DoubleValue;
                }

                set
                {
                    fields.fieldName3.DoubleValue = value;
                }
            }
            public DateTime TimestampValue
            {
                get
                {
                    return fields.fieldName4.TimestampValue;
                }

                set
                {
                    fields.fieldName4.TimestampValue = value;
                }
            }

            public override bool Equals(object other)
            {
                var o = other as JsonTest;
                return o != null
                    && StringValue == o.StringValue
                    && IntegerValue == o.IntegerValue
                    && DoubleValue == o.DoubleValue
                    && TimestampValue.Hour == o.TimestampValue.Hour
                    && TimestampValue.Minute == o.TimestampValue.Minute
                    && TimestampValue.Second == o.TimestampValue.Second;
            }
        }

        private class JsonTestFields
        {
            public JsonStringField fieldName1 { get; set; }
            public JsonNumericField fieldName2 { get; set; }
            public JsonNumericField fieldName3 { get; set; }
            public JsonDateTimeField fieldName4 { get; set; }
        }
    }
}

#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 168

namespace Utf8Json.Resolvers
{
    using System;
    using Utf8Json;

    public class GeneratedResolver : global::Utf8Json.IJsonFormatterResolver
    {
        public static readonly global::Utf8Json.IJsonFormatterResolver Instance = new GeneratedResolver();

        GeneratedResolver()
        {

        }

        public global::Utf8Json.IJsonFormatter<T> GetFormatter<T>()
        {
            return FormatterCache<T>.formatter;
        }

        static class FormatterCache<T>
        {
            public static readonly global::Utf8Json.IJsonFormatter<T> formatter;

            static FormatterCache()
            {
                var f = GeneratedResolverGetFormatterHelper.GetFormatter(typeof(T));
                if (f != null)
                {
                    formatter = (global::Utf8Json.IJsonFormatter<T>)f;
                }
            }
        }
    }

    internal static class GeneratedResolverGetFormatterHelper
    {
        static readonly global::System.Collections.Generic.Dictionary<Type, int> lookup;

        static GeneratedResolverGetFormatterHelper()
        {
            lookup = new global::System.Collections.Generic.Dictionary<Type, int>(16)
            {
                {typeof(global::System.Collections.Generic.List<global::kde.tech.InfoData.ApiInfo.Api>), 0 },
                {typeof(global::System.Collections.Generic.List<int>), 1 },
                {typeof(global::kde.tech.StoreData.Request), 2 },
                {typeof(global::kde.tech.StoreData.Response), 3 },
                {typeof(global::kde.tech.InfoData.Request), 4 },
                {typeof(global::kde.tech.InfoData.ApiInfo.Api), 5 },
                {typeof(global::kde.tech.InfoData.ApiInfo), 6 },
                {typeof(global::kde.tech.InfoData.Response), 7 },
                {typeof(global::kde.tech.UserData.Profile), 8 },
                {typeof(global::kde.tech.AuthData.Request), 9 },
                {typeof(global::kde.tech.AuthData.Response), 10 },
                {typeof(global::kde.tech.UDPingClient.Profile), 11 },
                {typeof(global::kde.tech.UDPingClient.PacketPayload), 12 },
                {typeof(global::kde.tech.UDPingClient.TestSession), 13 },
                {typeof(global::kde.tech.UDPingClient.TestResult), 14 },
                {typeof(global::kde.tech.UDPingClient.Profile.Node), 15 },
            };
        }

        internal static object GetFormatter(Type t)
        {
            int key;
            if (!lookup.TryGetValue(t, out key)) return null;

            switch (key)
            {
                case 0: return new global::Utf8Json.Formatters.ListFormatter<global::kde.tech.InfoData.ApiInfo.Api>();
                case 1: return new global::Utf8Json.Formatters.ListFormatter<int>();
                case 2: return new Utf8Json.Formatters.kde.tech.StoreData_RequestFormatter();
                case 3: return new Utf8Json.Formatters.kde.tech.StoreData_ResponseFormatter();
                case 4: return new Utf8Json.Formatters.kde.tech.InfoData_RequestFormatter();
                case 5: return new Utf8Json.Formatters.kde.tech.InfoData_ApiInfo_ApiFormatter();
                case 6: return new Utf8Json.Formatters.kde.tech.InfoData_ApiInfoFormatter();
                case 7: return new Utf8Json.Formatters.kde.tech.InfoData_ResponseFormatter();
                case 8: return new Utf8Json.Formatters.kde.tech.UserData_ProfileFormatter();
                case 9: return new Utf8Json.Formatters.kde.tech.AuthData_RequestFormatter();
                case 10: return new Utf8Json.Formatters.kde.tech.AuthData_ResponseFormatter();
                case 11: return new Utf8Json.Formatters.kde.tech.UDPingClient_ProfileFormatter();
                case 12: return new Utf8Json.Formatters.kde.tech.UDPingClient_PacketPayloadFormatter();
                case 13: return new Utf8Json.Formatters.kde.tech.UDPingClient_TestSessionFormatter();
                case 14: return new Utf8Json.Formatters.kde.tech.UDPingClient_TestResultFormatter();
                case 15: return new Utf8Json.Formatters.kde.tech.UDPingClient_Profile_NodeFormatter();
                default: return null;
            }
        }
    }
}

#pragma warning disable 168
#pragma warning restore 414
#pragma warning restore 618
#pragma warning restore 612

#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 219
#pragma warning disable 168

namespace Utf8Json.Formatters.kde.tech
{
    using System;
    using Utf8Json;


    public sealed class StoreData_RequestFormatter : global::Utf8Json.IJsonFormatter<global::kde.tech.StoreData.Request>
    {
        readonly global::Utf8Json.Internal.AutomataDictionary ____keyMapping;
        readonly byte[][] ____stringByteKeys;

        public StoreData_RequestFormatter()
        {
            this.____keyMapping = new global::Utf8Json.Internal.AutomataDictionary()
            {
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("role"), 0},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("mode"), 1},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("sessCode"), 2},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("dataName"), 3},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("dataIndex"), 4},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("dataBody"), 5},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("dataType"), 6},
            };

            this.____stringByteKeys = new byte[][]
            {
                JsonWriter.GetEncodedPropertyNameWithBeginObject("role"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("mode"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("sessCode"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("dataName"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("dataIndex"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("dataBody"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("dataType"),
                
            };
        }

        public void Serialize(ref JsonWriter writer, global::kde.tech.StoreData.Request value, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }
            

            writer.WriteRaw(this.____stringByteKeys[0]);
            writer.WriteString(value.role);
            writer.WriteRaw(this.____stringByteKeys[1]);
            writer.WriteString(value.mode);
            writer.WriteRaw(this.____stringByteKeys[2]);
            writer.WriteString(value.sessCode);
            writer.WriteRaw(this.____stringByteKeys[3]);
            writer.WriteString(value.dataName);
            writer.WriteRaw(this.____stringByteKeys[4]);
            writer.WriteInt32(value.dataIndex);
            writer.WriteRaw(this.____stringByteKeys[5]);
            writer.WriteString(value.dataBody);
            writer.WriteRaw(this.____stringByteKeys[6]);
            writer.WriteInt32(value.dataType);
            
            writer.WriteEndObject();
        }

        public global::kde.tech.StoreData.Request Deserialize(ref JsonReader reader, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
            {
                return null;
            }
            

            var __role__ = default(string);
            var __role__b__ = false;
            var __mode__ = default(string);
            var __mode__b__ = false;
            var __sessCode__ = default(string);
            var __sessCode__b__ = false;
            var __dataName__ = default(string);
            var __dataName__b__ = false;
            var __dataIndex__ = default(int);
            var __dataIndex__b__ = false;
            var __dataBody__ = default(string);
            var __dataBody__b__ = false;
            var __dataType__ = default(int);
            var __dataType__b__ = false;

            var ____count = 0;
            reader.ReadIsBeginObjectWithVerify();
            while (!reader.ReadIsEndObjectWithSkipValueSeparator(ref ____count))
            {
                var stringKey = reader.ReadPropertyNameSegmentRaw();
                int key;
                if (!____keyMapping.TryGetValueSafe(stringKey, out key))
                {
                    reader.ReadNextBlock();
                    goto NEXT_LOOP;
                }

                switch (key)
                {
                    case 0:
                        __role__ = reader.ReadString();
                        __role__b__ = true;
                        break;
                    case 1:
                        __mode__ = reader.ReadString();
                        __mode__b__ = true;
                        break;
                    case 2:
                        __sessCode__ = reader.ReadString();
                        __sessCode__b__ = true;
                        break;
                    case 3:
                        __dataName__ = reader.ReadString();
                        __dataName__b__ = true;
                        break;
                    case 4:
                        __dataIndex__ = reader.ReadInt32();
                        __dataIndex__b__ = true;
                        break;
                    case 5:
                        __dataBody__ = reader.ReadString();
                        __dataBody__b__ = true;
                        break;
                    case 6:
                        __dataType__ = reader.ReadInt32();
                        __dataType__b__ = true;
                        break;
                    default:
                        reader.ReadNextBlock();
                        break;
                }

                NEXT_LOOP:
                continue;
            }

            var ____result = new global::kde.tech.StoreData.Request();
            if(__role__b__) ____result.role = __role__;
            if(__mode__b__) ____result.mode = __mode__;
            if(__sessCode__b__) ____result.sessCode = __sessCode__;
            if(__dataName__b__) ____result.dataName = __dataName__;
            if(__dataIndex__b__) ____result.dataIndex = __dataIndex__;
            if(__dataBody__b__) ____result.dataBody = __dataBody__;
            if(__dataType__b__) ____result.dataType = __dataType__;

            return ____result;
        }
    }


    public sealed class StoreData_ResponseFormatter : global::Utf8Json.IJsonFormatter<global::kde.tech.StoreData.Response>
    {
        readonly global::Utf8Json.Internal.AutomataDictionary ____keyMapping;
        readonly byte[][] ____stringByteKeys;

        public StoreData_ResponseFormatter()
        {
            this.____keyMapping = new global::Utf8Json.Internal.AutomataDictionary()
            {
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("status"), 0},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("meta"), 1},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("dataBody"), 2},
            };

            this.____stringByteKeys = new byte[][]
            {
                JsonWriter.GetEncodedPropertyNameWithBeginObject("status"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("meta"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("dataBody"),
                
            };
        }

        public void Serialize(ref JsonWriter writer, global::kde.tech.StoreData.Response value, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }
            

            writer.WriteRaw(this.____stringByteKeys[0]);
            writer.WriteString(value.status);
            writer.WriteRaw(this.____stringByteKeys[1]);
            writer.WriteString(value.meta);
            writer.WriteRaw(this.____stringByteKeys[2]);
            writer.WriteString(value.dataBody);
            
            writer.WriteEndObject();
        }

        public global::kde.tech.StoreData.Response Deserialize(ref JsonReader reader, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
            {
                return null;
            }
            

            var __status__ = default(string);
            var __status__b__ = false;
            var __meta__ = default(string);
            var __meta__b__ = false;
            var __dataBody__ = default(string);
            var __dataBody__b__ = false;

            var ____count = 0;
            reader.ReadIsBeginObjectWithVerify();
            while (!reader.ReadIsEndObjectWithSkipValueSeparator(ref ____count))
            {
                var stringKey = reader.ReadPropertyNameSegmentRaw();
                int key;
                if (!____keyMapping.TryGetValueSafe(stringKey, out key))
                {
                    reader.ReadNextBlock();
                    goto NEXT_LOOP;
                }

                switch (key)
                {
                    case 0:
                        __status__ = reader.ReadString();
                        __status__b__ = true;
                        break;
                    case 1:
                        __meta__ = reader.ReadString();
                        __meta__b__ = true;
                        break;
                    case 2:
                        __dataBody__ = reader.ReadString();
                        __dataBody__b__ = true;
                        break;
                    default:
                        reader.ReadNextBlock();
                        break;
                }

                NEXT_LOOP:
                continue;
            }

            var ____result = new global::kde.tech.StoreData.Response();
            if(__status__b__) ____result.status = __status__;
            if(__meta__b__) ____result.meta = __meta__;
            if(__dataBody__b__) ____result.dataBody = __dataBody__;

            return ____result;
        }
    }


    public sealed class InfoData_RequestFormatter : global::Utf8Json.IJsonFormatter<global::kde.tech.InfoData.Request>
    {
        readonly global::Utf8Json.Internal.AutomataDictionary ____keyMapping;
        readonly byte[][] ____stringByteKeys;

        public InfoData_RequestFormatter()
        {
            this.____keyMapping = new global::Utf8Json.Internal.AutomataDictionary()
            {
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("role"), 0},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("mode"), 1},
            };

            this.____stringByteKeys = new byte[][]
            {
                JsonWriter.GetEncodedPropertyNameWithBeginObject("role"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("mode"),
                
            };
        }

        public void Serialize(ref JsonWriter writer, global::kde.tech.InfoData.Request value, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }
            

            writer.WriteRaw(this.____stringByteKeys[0]);
            writer.WriteString(value.role);
            writer.WriteRaw(this.____stringByteKeys[1]);
            writer.WriteString(value.mode);
            
            writer.WriteEndObject();
        }

        public global::kde.tech.InfoData.Request Deserialize(ref JsonReader reader, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
            {
                return null;
            }
            

            var __role__ = default(string);
            var __role__b__ = false;
            var __mode__ = default(string);
            var __mode__b__ = false;

            var ____count = 0;
            reader.ReadIsBeginObjectWithVerify();
            while (!reader.ReadIsEndObjectWithSkipValueSeparator(ref ____count))
            {
                var stringKey = reader.ReadPropertyNameSegmentRaw();
                int key;
                if (!____keyMapping.TryGetValueSafe(stringKey, out key))
                {
                    reader.ReadNextBlock();
                    goto NEXT_LOOP;
                }

                switch (key)
                {
                    case 0:
                        __role__ = reader.ReadString();
                        __role__b__ = true;
                        break;
                    case 1:
                        __mode__ = reader.ReadString();
                        __mode__b__ = true;
                        break;
                    default:
                        reader.ReadNextBlock();
                        break;
                }

                NEXT_LOOP:
                continue;
            }

            var ____result = new global::kde.tech.InfoData.Request();
            if(__role__b__) ____result.role = __role__;
            if(__mode__b__) ____result.mode = __mode__;

            return ____result;
        }
    }


    public sealed class InfoData_ApiInfo_ApiFormatter : global::Utf8Json.IJsonFormatter<global::kde.tech.InfoData.ApiInfo.Api>
    {
        readonly global::Utf8Json.Internal.AutomataDictionary ____keyMapping;
        readonly byte[][] ____stringByteKeys;

        public InfoData_ApiInfo_ApiFormatter()
        {
            this.____keyMapping = new global::Utf8Json.Internal.AutomataDictionary()
            {
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("name"), 0},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("url"), 1},
            };

            this.____stringByteKeys = new byte[][]
            {
                JsonWriter.GetEncodedPropertyNameWithBeginObject("name"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("url"),
                
            };
        }

        public void Serialize(ref JsonWriter writer, global::kde.tech.InfoData.ApiInfo.Api value, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }
            

            writer.WriteRaw(this.____stringByteKeys[0]);
            writer.WriteString(value.name);
            writer.WriteRaw(this.____stringByteKeys[1]);
            writer.WriteString(value.url);
            
            writer.WriteEndObject();
        }

        public global::kde.tech.InfoData.ApiInfo.Api Deserialize(ref JsonReader reader, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
            {
                return null;
            }
            

            var __name__ = default(string);
            var __name__b__ = false;
            var __url__ = default(string);
            var __url__b__ = false;

            var ____count = 0;
            reader.ReadIsBeginObjectWithVerify();
            while (!reader.ReadIsEndObjectWithSkipValueSeparator(ref ____count))
            {
                var stringKey = reader.ReadPropertyNameSegmentRaw();
                int key;
                if (!____keyMapping.TryGetValueSafe(stringKey, out key))
                {
                    reader.ReadNextBlock();
                    goto NEXT_LOOP;
                }

                switch (key)
                {
                    case 0:
                        __name__ = reader.ReadString();
                        __name__b__ = true;
                        break;
                    case 1:
                        __url__ = reader.ReadString();
                        __url__b__ = true;
                        break;
                    default:
                        reader.ReadNextBlock();
                        break;
                }

                NEXT_LOOP:
                continue;
            }

            var ____result = new global::kde.tech.InfoData.ApiInfo.Api();
            if(__name__b__) ____result.name = __name__;
            if(__url__b__) ____result.url = __url__;

            return ____result;
        }
    }


    public sealed class InfoData_ApiInfoFormatter : global::Utf8Json.IJsonFormatter<global::kde.tech.InfoData.ApiInfo>
    {
        readonly global::Utf8Json.Internal.AutomataDictionary ____keyMapping;
        readonly byte[][] ____stringByteKeys;

        public InfoData_ApiInfoFormatter()
        {
            this.____keyMapping = new global::Utf8Json.Internal.AutomataDictionary()
            {
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("lastupdate"), 0},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("key"), 1},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("list"), 2},
            };

            this.____stringByteKeys = new byte[][]
            {
                JsonWriter.GetEncodedPropertyNameWithBeginObject("lastupdate"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("key"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("list"),
                
            };
        }

        public void Serialize(ref JsonWriter writer, global::kde.tech.InfoData.ApiInfo value, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }
            

            writer.WriteRaw(this.____stringByteKeys[0]);
            writer.WriteInt32(value.lastupdate);
            writer.WriteRaw(this.____stringByteKeys[1]);
            writer.WriteString(value.key);
            writer.WriteRaw(this.____stringByteKeys[2]);
            formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.List<global::kde.tech.InfoData.ApiInfo.Api>>().Serialize(ref writer, value.list, formatterResolver);
            
            writer.WriteEndObject();
        }

        public global::kde.tech.InfoData.ApiInfo Deserialize(ref JsonReader reader, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
            {
                return null;
            }
            

            var __lastupdate__ = default(int);
            var __lastupdate__b__ = false;
            var __key__ = default(string);
            var __key__b__ = false;
            var __list__ = default(global::System.Collections.Generic.List<global::kde.tech.InfoData.ApiInfo.Api>);
            var __list__b__ = false;

            var ____count = 0;
            reader.ReadIsBeginObjectWithVerify();
            while (!reader.ReadIsEndObjectWithSkipValueSeparator(ref ____count))
            {
                var stringKey = reader.ReadPropertyNameSegmentRaw();
                int key;
                if (!____keyMapping.TryGetValueSafe(stringKey, out key))
                {
                    reader.ReadNextBlock();
                    goto NEXT_LOOP;
                }

                switch (key)
                {
                    case 0:
                        __lastupdate__ = reader.ReadInt32();
                        __lastupdate__b__ = true;
                        break;
                    case 1:
                        __key__ = reader.ReadString();
                        __key__b__ = true;
                        break;
                    case 2:
                        __list__ = formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.List<global::kde.tech.InfoData.ApiInfo.Api>>().Deserialize(ref reader, formatterResolver);
                        __list__b__ = true;
                        break;
                    default:
                        reader.ReadNextBlock();
                        break;
                }

                NEXT_LOOP:
                continue;
            }

            var ____result = new global::kde.tech.InfoData.ApiInfo();
            if(__lastupdate__b__) ____result.lastupdate = __lastupdate__;
            if(__key__b__) ____result.key = __key__;
            if(__list__b__) ____result.list = __list__;

            return ____result;
        }
    }


    public sealed class InfoData_ResponseFormatter : global::Utf8Json.IJsonFormatter<global::kde.tech.InfoData.Response>
    {
        readonly global::Utf8Json.Internal.AutomataDictionary ____keyMapping;
        readonly byte[][] ____stringByteKeys;

        public InfoData_ResponseFormatter()
        {
            this.____keyMapping = new global::Utf8Json.Internal.AutomataDictionary()
            {
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("status"), 0},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("apiInfo"), 1},
            };

            this.____stringByteKeys = new byte[][]
            {
                JsonWriter.GetEncodedPropertyNameWithBeginObject("status"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("apiInfo"),
                
            };
        }

        public void Serialize(ref JsonWriter writer, global::kde.tech.InfoData.Response value, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }
            

            writer.WriteRaw(this.____stringByteKeys[0]);
            writer.WriteString(value.status);
            writer.WriteRaw(this.____stringByteKeys[1]);
            formatterResolver.GetFormatterWithVerify<global::kde.tech.InfoData.ApiInfo>().Serialize(ref writer, value.apiInfo, formatterResolver);
            
            writer.WriteEndObject();
        }

        public global::kde.tech.InfoData.Response Deserialize(ref JsonReader reader, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
            {
                return null;
            }
            

            var __status__ = default(string);
            var __status__b__ = false;
            var __apiInfo__ = default(global::kde.tech.InfoData.ApiInfo);
            var __apiInfo__b__ = false;

            var ____count = 0;
            reader.ReadIsBeginObjectWithVerify();
            while (!reader.ReadIsEndObjectWithSkipValueSeparator(ref ____count))
            {
                var stringKey = reader.ReadPropertyNameSegmentRaw();
                int key;
                if (!____keyMapping.TryGetValueSafe(stringKey, out key))
                {
                    reader.ReadNextBlock();
                    goto NEXT_LOOP;
                }

                switch (key)
                {
                    case 0:
                        __status__ = reader.ReadString();
                        __status__b__ = true;
                        break;
                    case 1:
                        __apiInfo__ = formatterResolver.GetFormatterWithVerify<global::kde.tech.InfoData.ApiInfo>().Deserialize(ref reader, formatterResolver);
                        __apiInfo__b__ = true;
                        break;
                    default:
                        reader.ReadNextBlock();
                        break;
                }

                NEXT_LOOP:
                continue;
            }

            var ____result = new global::kde.tech.InfoData.Response();
            if(__status__b__) ____result.status = __status__;
            if(__apiInfo__b__) ____result.apiInfo = __apiInfo__;

            return ____result;
        }
    }


    public sealed class UserData_ProfileFormatter : global::Utf8Json.IJsonFormatter<global::kde.tech.UserData.Profile>
    {
        readonly global::Utf8Json.Internal.AutomataDictionary ____keyMapping;
        readonly byte[][] ____stringByteKeys;

        public UserData_ProfileFormatter()
        {
            this.____keyMapping = new global::Utf8Json.Internal.AutomataDictionary()
            {
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("guid"), 0},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("uid"), 1},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("name"), 2},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("ctime"), 3},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("mtime"), 4},
            };

            this.____stringByteKeys = new byte[][]
            {
                JsonWriter.GetEncodedPropertyNameWithBeginObject("guid"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("uid"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("name"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("ctime"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("mtime"),
                
            };
        }

        public void Serialize(ref JsonWriter writer, global::kde.tech.UserData.Profile value, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }
            

            writer.WriteRaw(this.____stringByteKeys[0]);
            writer.WriteString(value.guid);
            writer.WriteRaw(this.____stringByteKeys[1]);
            writer.WriteString(value.uid);
            writer.WriteRaw(this.____stringByteKeys[2]);
            writer.WriteString(value.name);
            writer.WriteRaw(this.____stringByteKeys[3]);
            writer.WriteInt32(value.ctime);
            writer.WriteRaw(this.____stringByteKeys[4]);
            writer.WriteInt32(value.mtime);
            
            writer.WriteEndObject();
        }

        public global::kde.tech.UserData.Profile Deserialize(ref JsonReader reader, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
            {
                return null;
            }
            

            var __guid__ = default(string);
            var __guid__b__ = false;
            var __uid__ = default(string);
            var __uid__b__ = false;
            var __name__ = default(string);
            var __name__b__ = false;
            var __ctime__ = default(int);
            var __ctime__b__ = false;
            var __mtime__ = default(int);
            var __mtime__b__ = false;

            var ____count = 0;
            reader.ReadIsBeginObjectWithVerify();
            while (!reader.ReadIsEndObjectWithSkipValueSeparator(ref ____count))
            {
                var stringKey = reader.ReadPropertyNameSegmentRaw();
                int key;
                if (!____keyMapping.TryGetValueSafe(stringKey, out key))
                {
                    reader.ReadNextBlock();
                    goto NEXT_LOOP;
                }

                switch (key)
                {
                    case 0:
                        __guid__ = reader.ReadString();
                        __guid__b__ = true;
                        break;
                    case 1:
                        __uid__ = reader.ReadString();
                        __uid__b__ = true;
                        break;
                    case 2:
                        __name__ = reader.ReadString();
                        __name__b__ = true;
                        break;
                    case 3:
                        __ctime__ = reader.ReadInt32();
                        __ctime__b__ = true;
                        break;
                    case 4:
                        __mtime__ = reader.ReadInt32();
                        __mtime__b__ = true;
                        break;
                    default:
                        reader.ReadNextBlock();
                        break;
                }

                NEXT_LOOP:
                continue;
            }

            var ____result = new global::kde.tech.UserData.Profile();
            if(__guid__b__) ____result.guid = __guid__;
            if(__uid__b__) ____result.uid = __uid__;
            if(__name__b__) ____result.name = __name__;
            if(__ctime__b__) ____result.ctime = __ctime__;
            if(__mtime__b__) ____result.mtime = __mtime__;

            return ____result;
        }
    }


    public sealed class AuthData_RequestFormatter : global::Utf8Json.IJsonFormatter<global::kde.tech.AuthData.Request>
    {
        readonly global::Utf8Json.Internal.AutomataDictionary ____keyMapping;
        readonly byte[][] ____stringByteKeys;

        public AuthData_RequestFormatter()
        {
            this.____keyMapping = new global::Utf8Json.Internal.AutomataDictionary()
            {
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("role"), 0},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("mode"), 1},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("userProfile"), 2},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("sessCode"), 3},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("takeoverCode"), 4},
            };

            this.____stringByteKeys = new byte[][]
            {
                JsonWriter.GetEncodedPropertyNameWithBeginObject("role"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("mode"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("userProfile"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("sessCode"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("takeoverCode"),
                
            };
        }

        public void Serialize(ref JsonWriter writer, global::kde.tech.AuthData.Request value, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }
            

            writer.WriteRaw(this.____stringByteKeys[0]);
            writer.WriteString(value.role);
            writer.WriteRaw(this.____stringByteKeys[1]);
            writer.WriteString(value.mode);
            writer.WriteRaw(this.____stringByteKeys[2]);
            formatterResolver.GetFormatterWithVerify<global::kde.tech.UserData.Profile>().Serialize(ref writer, value.userProfile, formatterResolver);
            writer.WriteRaw(this.____stringByteKeys[3]);
            writer.WriteString(value.sessCode);
            writer.WriteRaw(this.____stringByteKeys[4]);
            writer.WriteString(value.takeoverCode);
            
            writer.WriteEndObject();
        }

        public global::kde.tech.AuthData.Request Deserialize(ref JsonReader reader, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
            {
                return null;
            }
            

            var __role__ = default(string);
            var __role__b__ = false;
            var __mode__ = default(string);
            var __mode__b__ = false;
            var __userProfile__ = default(global::kde.tech.UserData.Profile);
            var __userProfile__b__ = false;
            var __sessCode__ = default(string);
            var __sessCode__b__ = false;
            var __takeoverCode__ = default(string);
            var __takeoverCode__b__ = false;

            var ____count = 0;
            reader.ReadIsBeginObjectWithVerify();
            while (!reader.ReadIsEndObjectWithSkipValueSeparator(ref ____count))
            {
                var stringKey = reader.ReadPropertyNameSegmentRaw();
                int key;
                if (!____keyMapping.TryGetValueSafe(stringKey, out key))
                {
                    reader.ReadNextBlock();
                    goto NEXT_LOOP;
                }

                switch (key)
                {
                    case 0:
                        __role__ = reader.ReadString();
                        __role__b__ = true;
                        break;
                    case 1:
                        __mode__ = reader.ReadString();
                        __mode__b__ = true;
                        break;
                    case 2:
                        __userProfile__ = formatterResolver.GetFormatterWithVerify<global::kde.tech.UserData.Profile>().Deserialize(ref reader, formatterResolver);
                        __userProfile__b__ = true;
                        break;
                    case 3:
                        __sessCode__ = reader.ReadString();
                        __sessCode__b__ = true;
                        break;
                    case 4:
                        __takeoverCode__ = reader.ReadString();
                        __takeoverCode__b__ = true;
                        break;
                    default:
                        reader.ReadNextBlock();
                        break;
                }

                NEXT_LOOP:
                continue;
            }

            var ____result = new global::kde.tech.AuthData.Request();
            if(__role__b__) ____result.role = __role__;
            if(__mode__b__) ____result.mode = __mode__;
            if(__userProfile__b__) ____result.userProfile = __userProfile__;
            if(__sessCode__b__) ____result.sessCode = __sessCode__;
            if(__takeoverCode__b__) ____result.takeoverCode = __takeoverCode__;

            return ____result;
        }
    }


    public sealed class AuthData_ResponseFormatter : global::Utf8Json.IJsonFormatter<global::kde.tech.AuthData.Response>
    {
        readonly global::Utf8Json.Internal.AutomataDictionary ____keyMapping;
        readonly byte[][] ____stringByteKeys;

        public AuthData_ResponseFormatter()
        {
            this.____keyMapping = new global::Utf8Json.Internal.AutomataDictionary()
            {
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("status"), 0},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("meta"), 1},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("sessCode"), 2},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("userProfile"), 3},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("takeoverCode"), 4},
            };

            this.____stringByteKeys = new byte[][]
            {
                JsonWriter.GetEncodedPropertyNameWithBeginObject("status"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("meta"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("sessCode"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("userProfile"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("takeoverCode"),
                
            };
        }

        public void Serialize(ref JsonWriter writer, global::kde.tech.AuthData.Response value, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }
            

            writer.WriteRaw(this.____stringByteKeys[0]);
            writer.WriteString(value.status);
            writer.WriteRaw(this.____stringByteKeys[1]);
            writer.WriteString(value.meta);
            writer.WriteRaw(this.____stringByteKeys[2]);
            writer.WriteString(value.sessCode);
            writer.WriteRaw(this.____stringByteKeys[3]);
            formatterResolver.GetFormatterWithVerify<global::kde.tech.UserData.Profile>().Serialize(ref writer, value.userProfile, formatterResolver);
            writer.WriteRaw(this.____stringByteKeys[4]);
            writer.WriteString(value.takeoverCode);
            
            writer.WriteEndObject();
        }

        public global::kde.tech.AuthData.Response Deserialize(ref JsonReader reader, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
            {
                return null;
            }
            

            var __status__ = default(string);
            var __status__b__ = false;
            var __meta__ = default(string);
            var __meta__b__ = false;
            var __sessCode__ = default(string);
            var __sessCode__b__ = false;
            var __userProfile__ = default(global::kde.tech.UserData.Profile);
            var __userProfile__b__ = false;
            var __takeoverCode__ = default(string);
            var __takeoverCode__b__ = false;

            var ____count = 0;
            reader.ReadIsBeginObjectWithVerify();
            while (!reader.ReadIsEndObjectWithSkipValueSeparator(ref ____count))
            {
                var stringKey = reader.ReadPropertyNameSegmentRaw();
                int key;
                if (!____keyMapping.TryGetValueSafe(stringKey, out key))
                {
                    reader.ReadNextBlock();
                    goto NEXT_LOOP;
                }

                switch (key)
                {
                    case 0:
                        __status__ = reader.ReadString();
                        __status__b__ = true;
                        break;
                    case 1:
                        __meta__ = reader.ReadString();
                        __meta__b__ = true;
                        break;
                    case 2:
                        __sessCode__ = reader.ReadString();
                        __sessCode__b__ = true;
                        break;
                    case 3:
                        __userProfile__ = formatterResolver.GetFormatterWithVerify<global::kde.tech.UserData.Profile>().Deserialize(ref reader, formatterResolver);
                        __userProfile__b__ = true;
                        break;
                    case 4:
                        __takeoverCode__ = reader.ReadString();
                        __takeoverCode__b__ = true;
                        break;
                    default:
                        reader.ReadNextBlock();
                        break;
                }

                NEXT_LOOP:
                continue;
            }

            var ____result = new global::kde.tech.AuthData.Response();
            if(__status__b__) ____result.status = __status__;
            if(__meta__b__) ____result.meta = __meta__;
            if(__sessCode__b__) ____result.sessCode = __sessCode__;
            if(__userProfile__b__) ____result.userProfile = __userProfile__;
            if(__takeoverCode__b__) ____result.takeoverCode = __takeoverCode__;

            return ____result;
        }
    }


    public sealed class UDPingClient_ProfileFormatter : global::Utf8Json.IJsonFormatter<global::kde.tech.UDPingClient.Profile>
    {
        readonly global::Utf8Json.Internal.AutomataDictionary ____keyMapping;
        readonly byte[][] ____stringByteKeys;

        public UDPingClient_ProfileFormatter()
        {
            this.____keyMapping = new global::Utf8Json.Internal.AutomataDictionary()
            {
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("status"), 0},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("pps"), 1},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("pktSize"), 2},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("duration"), 3},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("now"), 4},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("currentSeq"), 5},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("median"), 6},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("avg"), 7},
            };

            this.____stringByteKeys = new byte[][]
            {
                JsonWriter.GetEncodedPropertyNameWithBeginObject("status"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("pps"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("pktSize"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("duration"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("now"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("currentSeq"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("median"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("avg"),
                
            };
        }

        public void Serialize(ref JsonWriter writer, global::kde.tech.UDPingClient.Profile value, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }
            

            writer.WriteRaw(this.____stringByteKeys[0]);
            formatterResolver.GetFormatterWithVerify<global::kde.tech.UDPingClient.Status>().Serialize(ref writer, value.status, formatterResolver);
            writer.WriteRaw(this.____stringByteKeys[1]);
            writer.WriteInt32(value.pps);
            writer.WriteRaw(this.____stringByteKeys[2]);
            writer.WriteInt32(value.pktSize);
            writer.WriteRaw(this.____stringByteKeys[3]);
            writer.WriteInt32(value.duration);
            writer.WriteRaw(this.____stringByteKeys[4]);
            writer.WriteInt64(value.now);
            writer.WriteRaw(this.____stringByteKeys[5]);
            writer.WriteInt32(value.currentSeq);
            writer.WriteRaw(this.____stringByteKeys[6]);
            writer.WriteInt32(value.median);
            writer.WriteRaw(this.____stringByteKeys[7]);
            writer.WriteInt32(value.avg);
            
            writer.WriteEndObject();
        }

        public global::kde.tech.UDPingClient.Profile Deserialize(ref JsonReader reader, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
            {
                return null;
            }
            

            var __status__ = default(global::kde.tech.UDPingClient.Status);
            var __status__b__ = false;
            var __pps__ = default(int);
            var __pps__b__ = false;
            var __pktSize__ = default(int);
            var __pktSize__b__ = false;
            var __duration__ = default(int);
            var __duration__b__ = false;
            var __now__ = default(long);
            var __now__b__ = false;
            var __currentSeq__ = default(int);
            var __currentSeq__b__ = false;
            var __median__ = default(int);
            var __median__b__ = false;
            var __avg__ = default(int);
            var __avg__b__ = false;

            var ____count = 0;
            reader.ReadIsBeginObjectWithVerify();
            while (!reader.ReadIsEndObjectWithSkipValueSeparator(ref ____count))
            {
                var stringKey = reader.ReadPropertyNameSegmentRaw();
                int key;
                if (!____keyMapping.TryGetValueSafe(stringKey, out key))
                {
                    reader.ReadNextBlock();
                    goto NEXT_LOOP;
                }

                switch (key)
                {
                    case 0:
                        __status__ = formatterResolver.GetFormatterWithVerify<global::kde.tech.UDPingClient.Status>().Deserialize(ref reader, formatterResolver);
                        __status__b__ = true;
                        break;
                    case 1:
                        __pps__ = reader.ReadInt32();
                        __pps__b__ = true;
                        break;
                    case 2:
                        __pktSize__ = reader.ReadInt32();
                        __pktSize__b__ = true;
                        break;
                    case 3:
                        __duration__ = reader.ReadInt32();
                        __duration__b__ = true;
                        break;
                    case 4:
                        __now__ = reader.ReadInt64();
                        __now__b__ = true;
                        break;
                    case 5:
                        __currentSeq__ = reader.ReadInt32();
                        __currentSeq__b__ = true;
                        break;
                    case 6:
                        __median__ = reader.ReadInt32();
                        __median__b__ = true;
                        break;
                    case 7:
                        __avg__ = reader.ReadInt32();
                        __avg__b__ = true;
                        break;
                    default:
                        reader.ReadNextBlock();
                        break;
                }

                NEXT_LOOP:
                continue;
            }

            var ____result = new global::kde.tech.UDPingClient.Profile();
            if(__status__b__) ____result.status = __status__;
            if(__pps__b__) ____result.pps = __pps__;
            if(__pktSize__b__) ____result.pktSize = __pktSize__;
            if(__duration__b__) ____result.duration = __duration__;
            if(__currentSeq__b__) ____result.currentSeq = __currentSeq__;

            return ____result;
        }
    }


    public sealed class UDPingClient_PacketPayloadFormatter : global::Utf8Json.IJsonFormatter<global::kde.tech.UDPingClient.PacketPayload>
    {
        readonly global::Utf8Json.Internal.AutomataDictionary ____keyMapping;
        readonly byte[][] ____stringByteKeys;

        public UDPingClient_PacketPayloadFormatter()
        {
            this.____keyMapping = new global::Utf8Json.Internal.AutomataDictionary()
            {
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("msg"), 0},
            };

            this.____stringByteKeys = new byte[][]
            {
                JsonWriter.GetEncodedPropertyNameWithBeginObject("msg"),
                
            };
        }

        public void Serialize(ref JsonWriter writer, global::kde.tech.UDPingClient.PacketPayload value, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }
            

            writer.WriteRaw(this.____stringByteKeys[0]);
            writer.WriteString(value.msg);
            
            writer.WriteEndObject();
        }

        public global::kde.tech.UDPingClient.PacketPayload Deserialize(ref JsonReader reader, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
            {
                return null;
            }
            

            var __msg__ = default(string);
            var __msg__b__ = false;

            var ____count = 0;
            reader.ReadIsBeginObjectWithVerify();
            while (!reader.ReadIsEndObjectWithSkipValueSeparator(ref ____count))
            {
                var stringKey = reader.ReadPropertyNameSegmentRaw();
                int key;
                if (!____keyMapping.TryGetValueSafe(stringKey, out key))
                {
                    reader.ReadNextBlock();
                    goto NEXT_LOOP;
                }

                switch (key)
                {
                    case 0:
                        __msg__ = reader.ReadString();
                        __msg__b__ = true;
                        break;
                    default:
                        reader.ReadNextBlock();
                        break;
                }

                NEXT_LOOP:
                continue;
            }

            var ____result = new global::kde.tech.UDPingClient.PacketPayload();
            if(__msg__b__) ____result.msg = __msg__;

            return ____result;
        }
    }


    public sealed class UDPingClient_TestSessionFormatter : global::Utf8Json.IJsonFormatter<global::kde.tech.UDPingClient.TestSession>
    {
        readonly global::Utf8Json.Internal.AutomataDictionary ____keyMapping;
        readonly byte[][] ____stringByteKeys;

        public UDPingClient_TestSessionFormatter()
        {
            this.____keyMapping = new global::Utf8Json.Internal.AutomataDictionary()
            {
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("sendTime"), 0},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("recvTime"), 1},
            };

            this.____stringByteKeys = new byte[][]
            {
                JsonWriter.GetEncodedPropertyNameWithBeginObject("sendTime"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("recvTime"),
                
            };
        }

        public void Serialize(ref JsonWriter writer, global::kde.tech.UDPingClient.TestSession value, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }
            

            writer.WriteRaw(this.____stringByteKeys[0]);
            writer.WriteInt32(value.sendTime);
            writer.WriteRaw(this.____stringByteKeys[1]);
            writer.WriteInt32(value.recvTime);
            
            writer.WriteEndObject();
        }

        public global::kde.tech.UDPingClient.TestSession Deserialize(ref JsonReader reader, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
            {
                return null;
            }
            

            var __sendTime__ = default(int);
            var __sendTime__b__ = false;
            var __recvTime__ = default(int);
            var __recvTime__b__ = false;

            var ____count = 0;
            reader.ReadIsBeginObjectWithVerify();
            while (!reader.ReadIsEndObjectWithSkipValueSeparator(ref ____count))
            {
                var stringKey = reader.ReadPropertyNameSegmentRaw();
                int key;
                if (!____keyMapping.TryGetValueSafe(stringKey, out key))
                {
                    reader.ReadNextBlock();
                    goto NEXT_LOOP;
                }

                switch (key)
                {
                    case 0:
                        __sendTime__ = reader.ReadInt32();
                        __sendTime__b__ = true;
                        break;
                    case 1:
                        __recvTime__ = reader.ReadInt32();
                        __recvTime__b__ = true;
                        break;
                    default:
                        reader.ReadNextBlock();
                        break;
                }

                NEXT_LOOP:
                continue;
            }

            var ____result = new global::kde.tech.UDPingClient.TestSession();
            if(__sendTime__b__) ____result.sendTime = __sendTime__;
            if(__recvTime__b__) ____result.recvTime = __recvTime__;

            return ____result;
        }
    }


    public sealed class UDPingClient_TestResultFormatter : global::Utf8Json.IJsonFormatter<global::kde.tech.UDPingClient.TestResult>
    {
        readonly global::Utf8Json.Internal.AutomataDictionary ____keyMapping;
        readonly byte[][] ____stringByteKeys;

        public UDPingClient_TestResultFormatter()
        {
            this.____keyMapping = new global::Utf8Json.Internal.AutomataDictionary()
            {
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("timestamp"), 0},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("pps"), 1},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("pktSize"), 2},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("duration"), 3},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("rtt"), 4},
            };

            this.____stringByteKeys = new byte[][]
            {
                JsonWriter.GetEncodedPropertyNameWithBeginObject("timestamp"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("pps"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("pktSize"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("duration"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("rtt"),
                
            };
        }

        public void Serialize(ref JsonWriter writer, global::kde.tech.UDPingClient.TestResult value, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }
            

            writer.WriteRaw(this.____stringByteKeys[0]);
            writer.WriteInt32(value.timestamp);
            writer.WriteRaw(this.____stringByteKeys[1]);
            writer.WriteInt32(value.pps);
            writer.WriteRaw(this.____stringByteKeys[2]);
            writer.WriteInt32(value.pktSize);
            writer.WriteRaw(this.____stringByteKeys[3]);
            writer.WriteInt32(value.duration);
            writer.WriteRaw(this.____stringByteKeys[4]);
            formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.List<int>>().Serialize(ref writer, value.rtt, formatterResolver);
            
            writer.WriteEndObject();
        }

        public global::kde.tech.UDPingClient.TestResult Deserialize(ref JsonReader reader, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
            {
                return null;
            }
            

            var __timestamp__ = default(int);
            var __timestamp__b__ = false;
            var __pps__ = default(int);
            var __pps__b__ = false;
            var __pktSize__ = default(int);
            var __pktSize__b__ = false;
            var __duration__ = default(int);
            var __duration__b__ = false;
            var __rtt__ = default(global::System.Collections.Generic.List<int>);
            var __rtt__b__ = false;

            var ____count = 0;
            reader.ReadIsBeginObjectWithVerify();
            while (!reader.ReadIsEndObjectWithSkipValueSeparator(ref ____count))
            {
                var stringKey = reader.ReadPropertyNameSegmentRaw();
                int key;
                if (!____keyMapping.TryGetValueSafe(stringKey, out key))
                {
                    reader.ReadNextBlock();
                    goto NEXT_LOOP;
                }

                switch (key)
                {
                    case 0:
                        __timestamp__ = reader.ReadInt32();
                        __timestamp__b__ = true;
                        break;
                    case 1:
                        __pps__ = reader.ReadInt32();
                        __pps__b__ = true;
                        break;
                    case 2:
                        __pktSize__ = reader.ReadInt32();
                        __pktSize__b__ = true;
                        break;
                    case 3:
                        __duration__ = reader.ReadInt32();
                        __duration__b__ = true;
                        break;
                    case 4:
                        __rtt__ = formatterResolver.GetFormatterWithVerify<global::System.Collections.Generic.List<int>>().Deserialize(ref reader, formatterResolver);
                        __rtt__b__ = true;
                        break;
                    default:
                        reader.ReadNextBlock();
                        break;
                }

                NEXT_LOOP:
                continue;
            }

            var ____result = new global::kde.tech.UDPingClient.TestResult();
            if(__timestamp__b__) ____result.timestamp = __timestamp__;
            if(__pps__b__) ____result.pps = __pps__;
            if(__pktSize__b__) ____result.pktSize = __pktSize__;
            if(__duration__b__) ____result.duration = __duration__;
            if(__rtt__b__) ____result.rtt = __rtt__;

            return ____result;
        }
    }


    public sealed class UDPingClient_Profile_NodeFormatter : global::Utf8Json.IJsonFormatter<global::kde.tech.UDPingClient.Profile.Node>
    {
        readonly global::Utf8Json.Internal.AutomataDictionary ____keyMapping;
        readonly byte[][] ____stringByteKeys;

        public UDPingClient_Profile_NodeFormatter()
        {
            this.____keyMapping = new global::Utf8Json.Internal.AutomataDictionary()
            {
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("seq"), 0},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("sendTime"), 1},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("recvTime"), 2},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("isTimeout"), 3},
                { JsonWriter.GetEncodedPropertyNameWithoutQuotation("rtt"), 4},
            };

            this.____stringByteKeys = new byte[][]
            {
                JsonWriter.GetEncodedPropertyNameWithBeginObject("seq"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("sendTime"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("recvTime"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("isTimeout"),
                JsonWriter.GetEncodedPropertyNameWithPrefixValueSeparator("rtt"),
                
            };
        }

        public void Serialize(ref JsonWriter writer, global::kde.tech.UDPingClient.Profile.Node value, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }
            

            writer.WriteRaw(this.____stringByteKeys[0]);
            writer.WriteInt32(value.seq);
            writer.WriteRaw(this.____stringByteKeys[1]);
            writer.WriteInt64(value.sendTime);
            writer.WriteRaw(this.____stringByteKeys[2]);
            writer.WriteInt64(value.recvTime);
            writer.WriteRaw(this.____stringByteKeys[3]);
            writer.WriteBoolean(value.isTimeout);
            writer.WriteRaw(this.____stringByteKeys[4]);
            writer.WriteInt32(value.rtt);
            
            writer.WriteEndObject();
        }

        public global::kde.tech.UDPingClient.Profile.Node Deserialize(ref JsonReader reader, global::Utf8Json.IJsonFormatterResolver formatterResolver)
        {
            if (reader.ReadIsNull())
            {
                return null;
            }
            

            var __seq__ = default(int);
            var __seq__b__ = false;
            var __sendTime__ = default(long);
            var __sendTime__b__ = false;
            var __recvTime__ = default(long);
            var __recvTime__b__ = false;
            var __isTimeout__ = default(bool);
            var __isTimeout__b__ = false;
            var __rtt__ = default(int);
            var __rtt__b__ = false;

            var ____count = 0;
            reader.ReadIsBeginObjectWithVerify();
            while (!reader.ReadIsEndObjectWithSkipValueSeparator(ref ____count))
            {
                var stringKey = reader.ReadPropertyNameSegmentRaw();
                int key;
                if (!____keyMapping.TryGetValueSafe(stringKey, out key))
                {
                    reader.ReadNextBlock();
                    goto NEXT_LOOP;
                }

                switch (key)
                {
                    case 0:
                        __seq__ = reader.ReadInt32();
                        __seq__b__ = true;
                        break;
                    case 1:
                        __sendTime__ = reader.ReadInt64();
                        __sendTime__b__ = true;
                        break;
                    case 2:
                        __recvTime__ = reader.ReadInt64();
                        __recvTime__b__ = true;
                        break;
                    case 3:
                        __isTimeout__ = reader.ReadBoolean();
                        __isTimeout__b__ = true;
                        break;
                    case 4:
                        __rtt__ = reader.ReadInt32();
                        __rtt__b__ = true;
                        break;
                    default:
                        reader.ReadNextBlock();
                        break;
                }

                NEXT_LOOP:
                continue;
            }

            var ____result = new global::kde.tech.UDPingClient.Profile.Node();
            if(__seq__b__) ____result.seq = __seq__;
            if(__sendTime__b__) ____result.sendTime = __sendTime__;
            if(__recvTime__b__) ____result.recvTime = __recvTime__;
            if(__isTimeout__b__) ____result.isTimeout = __isTimeout__;

            return ____result;
        }
    }

}

#pragma warning disable 168
#pragma warning restore 219
#pragma warning restore 414
#pragma warning restore 618
#pragma warning restore 612

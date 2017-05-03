using MadWare.Passbook.Fields;
using MadWare.Passbook.PassStyle;
using MadWare.Passbook.Template;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MadWare.Passbook.Tests
{
    public class PassTemplateTests
    {
        [Fact]
        public void BasicTemplate()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory; //Assembly.GetExecutingAssembly().Location;
            var cwd = new DirectoryInfo(path).Parent.Parent.Parent;
            path = cwd.FullName;

            var images = new SerializableDictionary<Enums.PassbookImageType, byte[]>();
            images.Add(Enums.PassbookImageType.Icon, File.ReadAllBytes(path + "\\resources\\eon4u-logo.png"));
            images.Add(Enums.PassbookImageType.IconRetina, File.ReadAllBytes(path + "\\resources\\eon4u-logo.png"));
            images.Add(Enums.PassbookImageType.LogoRetina, File.ReadAllBytes(path + "\\resources\\eon4u-logo.png"));
            images.Add(Enums.PassbookImageType.Logo, File.ReadAllBytes(path + "\\resources\\eon4u-logo.png"));

            var p = new Pass<StoreCardPassStyle>
            {
                SerialNumber = "123456789",
                Description = "test",
                OrganizationName = "Test",
                BackgroundColor = "rgb(101,51,113)",
                LabelColor = "rgb(255,255,255)",
                ForegroundColor = "rgb(255,255,255)",
                PassStyle = new StoreCardPassStyle
                {

                    PrimaryFields = new List<Field>
                        {
                            new StandardField("name", "Anže", "Kravanja")
                        }

                },
                Images = images
            };

            XmlPassTemplate template = new XmlPassTemplate();
            string test = template.SaveTemplate(p);
            var deserializePass = template.ReadTemplate<StoreCardPassStyle>(test);
            string test2 = template.SaveTemplate(deserializePass);
            var isEqual = AreObjectsEqual(deserializePass, p);
            

        }

        public bool AreObjectsEqual(object objectA, object objectB)
        {
            bool result;

            if (objectA != null && objectB != null)
            {
                Type objectType;

                objectType = objectA.GetType();

                result = true; 

                foreach (PropertyInfo propertyInfo in objectType.GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => p.CanRead))
                {
                    object valueA;
                    object valueB;

                    valueA = propertyInfo.GetValue(objectA, null);
                    valueB = propertyInfo.GetValue(objectB, null);

                  
                    if (CanDirectlyCompare(propertyInfo.PropertyType))
                    {
                        if (!AreValuesEqual(valueA, valueB))
                        {
                            Console.WriteLine("Mismatch with property '{0}.{1}' found.",
                                        objectType.FullName, propertyInfo.Name);
                            result = false;
                        }
                    }

                    else if (typeof(IEnumerable).IsAssignableFrom(propertyInfo.PropertyType))
                    {
                        IEnumerable<object> collectionItems1;
                        IEnumerable<object> collectionItems2;
                        int collectionItemsCount1;
                        int collectionItemsCount2;
                        

                       
                        if (valueA != null && valueB != null)
                        {
                            collectionItems1 = ((IEnumerable)valueA).Cast<object>();
                            collectionItems2 = ((IEnumerable)valueB).Cast<object>();
                            collectionItemsCount1 = collectionItems1.Count();
                            collectionItemsCount2 = collectionItems2.Count();

                          
                            if (collectionItemsCount1 != collectionItemsCount2)
                            {
                                result = false;
                            }
                            
                            else
                            {
                                for (int i = 0; i < collectionItemsCount1; i++)
                                {
                                    object collectionItem1;
                                    object collectionItem2;
                                    Type collectionItemType;

                                    collectionItem1 = collectionItems1.ElementAt(i);
                                    collectionItem2 = collectionItems2.ElementAt(i);
                                    collectionItemType = collectionItem1.GetType();

                                    if (CanDirectlyCompare(collectionItemType))
                                    {
                                        if (!AreValuesEqual(collectionItem1, collectionItem2))
                                        {
                                            result = false;
                                        }
                                    }
                                    else if (!AreObjectsEqual(collectionItem1, collectionItem2))
                                    {
                                        result = false;
                                    }
                                }
                            }
                        }
                    }
                    else if (propertyInfo.PropertyType.IsClass)
                    {
                        if (!AreObjectsEqual(propertyInfo.GetValue(objectA, null), propertyInfo.GetValue(objectB, null)))
                        {

                            result = false;
                        }
                    }
                    else
                    {
                        result = false;
                    }
                }
            }
            else
                result = object.Equals(objectA, objectB);

            return result;
        }

        private static bool CanDirectlyCompare(Type type)
        {
            return typeof(IComparable).IsAssignableFrom(type) || type.IsPrimitive || type.IsValueType;
        }

        private static bool AreValuesEqual(object valueA, object valueB)
        {
            bool result;
            IComparable selfValueComparer;

            selfValueComparer = valueA as IComparable;

            if (valueA == null && valueB != null || valueA != null && valueB == null)
                result = false; 
            
           
            else
                result = true;

            return result;
        }
    }
}




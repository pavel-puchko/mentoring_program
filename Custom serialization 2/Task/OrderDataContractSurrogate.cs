using System;
using System.CodeDom;
using System.Collections;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Objects;
using System.Reflection;
using System.Runtime.Serialization;

using Task.DB;

namespace Task
{
    public class OrderDataContractSurrogate : IDataContractSurrogate
    {
        public object GetCustomDataToExport(Type clrType, Type dataContractType)
        {
            throw new NotImplementedException();
        }

        public object GetCustomDataToExport(MemberInfo memberInfo, Type dataContractType)
        {
            throw new NotImplementedException();
        }
        public Type GetReferencedTypeOnImport(string typeName, string typeNamespace, object customData)
        {
            throw new NotImplementedException();
        }

        public CodeTypeDeclaration ProcessImportedType(CodeTypeDeclaration typeDeclaration, CodeCompileUnit compileUnit)
        {
            throw new NotImplementedException();
        }
        public void GetKnownCustomDataTypes(Collection<Type> customDataTypes)
        {
            throw new NotImplementedException();
        }

        public Type GetDataContractType(Type type)
        {
            Type t = ObjectContext.GetObjectType(type);

            if (t == typeof(Order) || 
                t == typeof(Customer) || 
                t == typeof(Shipper) || 
                t == typeof(Order_Detail) ||
                t == typeof(Employee))
            {
                return t;
            }

            return type;
        }

        public object GetObjectToSerialize(object obj, Type targetType)
        {
            if (targetType == typeof(Shipper) ||
                targetType == typeof(Order_Detail) ||
                targetType == typeof(Employee) ||
                targetType == typeof(ICollection))
            {
                return null;
            }
            else if (targetType == typeof(Order))
            {
                return this.MapOrder(obj);
            }
            else if (targetType == typeof(Customer))
            {
                return this.MapCustomer(obj);
            }

            return obj;
        }

        public object GetDeserializedObject(object obj, Type targetType)
        {
            return obj;
        }

        private Customer MapCustomer(object obj)
        {
            Customer source = obj as Customer;

            Customer result = new Customer()
            {
                CustomerID = source.CustomerID,
                CompanyName = source.CompanyName
            };

            return result;
        }

        private Order MapOrder(object obj)
        {
            Order orderSource = obj as Order;
            Customer customer = this.MapCustomer(orderSource.Customer);

            Order result = new Order()
            {
                CustomerID = orderSource.CustomerID,
                OrderID = orderSource.OrderID,
                Customer = customer
            };

            return result;
        }
    }
}

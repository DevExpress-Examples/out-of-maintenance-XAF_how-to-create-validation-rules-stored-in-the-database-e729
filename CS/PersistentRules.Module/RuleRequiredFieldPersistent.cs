using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;

using System.Reflection;

namespace MainDemo.Module {
    [DefaultClassOptions]
    public class RuleRequiredFieldPersistent : BaseObject, DevExpress.Persistent.Validation.IRuleSource {
        public RuleRequiredFieldPersistent(Session session) : base(session) { }
        public string RuleName {
            get { return GetPropertyValue<string>("RuleName"); }
            set { SetPropertyValue("RuleName", value); }
        }
        public string CustomMessageTemplate {
            get { return GetPropertyValue<string>("CustomMessageTemplate"); }
            set { SetPropertyValue("CustomMessageTemplate", value); }
        }
        public bool SkipNullOrEmptyValues {
            get { return GetPropertyValue<bool>("SkipNullOrEmptyValues"); }
            set { SetPropertyValue("SkipNullOrEmptyValues", value); }
        }
        public string Id {
            get { return GetPropertyValue<string>("Id"); }
            set { SetPropertyValue("Id", value); }
        }
        public bool InvertResult {
            get { return GetPropertyValue<bool>("InvertResult"); }
            set { SetPropertyValue("InvertResult", value); }
        }
        public string ContextIDs {
            get { return GetPropertyValue<string>("ContextIDs"); }
            set { SetPropertyValue("ContextIDs", value); }
        }
        public string Property {
            get { return GetPropertyValue<string>("Property"); }
            set { SetPropertyValue("Property", value); }
        }
        [Persistent("ObjectType")]
        protected string ObjectType {
            get {
                if(ObjectTypeCore != null) {
                    return ObjectTypeCore.FullName;
                }
                return "";
            }
            set { ObjectTypeCore = ReflectionHelper.FindType(value); }
        }
        [NonPersistent]
        [TypeConverter(typeof(DevExpress.Persistent.Base.LocalizedClassInfoTypeConverter))]
        public Type ObjectTypeCore {
            get { return GetPropertyValue<Type>("ObjectTypeCore"); }
            set { SetPropertyValue("ObjectTypeCore", value); }
        }
        #region IRuleSource Members
        public System.Collections.Generic.ICollection<IRule> CreateRules() {
            System.Collections.Generic.List<IRule> list = new System.Collections.Generic.List<IRule>();
            RuleRequiredField rule = new RuleRequiredField();
            rule.Properties.SkipNullOrEmptyValues = this.SkipNullOrEmptyValues;
            rule.Properties.Id = this.Id;
            rule.Properties.InvertResult = this.InvertResult;
            rule.Properties.CustomMessageTemplate = this.CustomMessageTemplate;
            rule.Properties.TargetContextIDs = this.ContextIDs;
            rule.Properties.TargetType = this.ObjectTypeCore;
            if(rule.Properties.TargetType != null) {
                foreach(PropertyInfo pi in rule.Properties.TargetType.GetProperties()) {
                    if(pi.Name == this.Property) {
                        rule.Properties.TargetPropertyName = pi.Name;
                    }
                }
            }
            for(int i = Validator.RuleSet.RegisteredRules.Count - 1; i >= 0; i--) {
                if(Validator.RuleSet.RegisteredRules[i].Id == this.Id) {
                    Validator.RuleSet.RegisteredRules.RemoveAt(i);
                }
            }
            list.Add(rule);
            return list;
        }
        [Browsable(false)]
        public string Name {
            get { return this.RuleName; }
        }
        #endregion
    }
}

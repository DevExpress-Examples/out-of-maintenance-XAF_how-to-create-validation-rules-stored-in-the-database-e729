using System;
using DevExpress.ExpressApp.Updating;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.BaseImpl;
using MainDemo.Module;

using DevExpress.ExpressApp;
using DevExpress.Persistent.Validation;

namespace PersistentRules.Module {
    public class Updater : ModuleUpdater {
        public Updater(IObjectSpace objectSpace, Version currentDBVersion) : base(objectSpace, currentDBVersion) { }
        public override void UpdateDatabaseAfterUpdateSchema() {
            base.UpdateDatabaseAfterUpdateSchema();

            Person Sam = ObjectSpace.CreateObject<Person>();
            Sam.FirstName = "Sam";
            Sam.Save();

            RuleRequiredFieldPersistent LastNameIsNotEmpty = ObjectSpace.CreateObject<RuleRequiredFieldPersistent>();
            LastNameIsNotEmpty.ObjectTypeCore = typeof(Person);
            LastNameIsNotEmpty.Property = "LastName";
            LastNameIsNotEmpty.ContextIDs = "Save";
            LastNameIsNotEmpty.InvertResult = false;
            LastNameIsNotEmpty.Id = "Person_LastNameIsNotEmpty";
            LastNameIsNotEmpty.SkipNullOrEmptyValues = false;
            LastNameIsNotEmpty.RuleName = LastNameIsNotEmpty.Id;
            LastNameIsNotEmpty.Save();
        }
    }
}

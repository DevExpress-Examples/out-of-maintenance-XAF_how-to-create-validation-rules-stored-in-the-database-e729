Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.ExpressApp.Updating
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports DevExpress.Persistent.BaseImpl
Imports MainDemo.Module

Imports DevExpress.ExpressApp
Imports DevExpress.Persistent.Validation

Namespace PersistentRules.Module
	Public Class Updater
		Inherits ModuleUpdater
		Public Sub New(ByVal objectSpace As IObjectSpace, ByVal currentDBVersion As Version)
			MyBase.New(objectSpace, currentDBVersion)
		End Sub
		Public Overrides Sub UpdateDatabaseAfterUpdateSchema()
			MyBase.UpdateDatabaseAfterUpdateSchema()

			Dim Sam As Person = ObjectSpace.CreateObject(Of Person)()
			Sam.FirstName = "Sam"
			Sam.Save()

			Dim LastNameIsNotEmpty As RuleRequiredFieldPersistent = ObjectSpace.CreateObject(Of RuleRequiredFieldPersistent)()
			LastNameIsNotEmpty.ObjectTypeCore = GetType(Person)
			LastNameIsNotEmpty.Property = "LastName"
			LastNameIsNotEmpty.ContextIDs = "Save"
			LastNameIsNotEmpty.InvertResult = False
			LastNameIsNotEmpty.Id = "Person_LastNameIsNotEmpty"
			LastNameIsNotEmpty.SkipNullOrEmptyValues = False
			LastNameIsNotEmpty.RuleName = LastNameIsNotEmpty.Id
			LastNameIsNotEmpty.Save()
		End Sub
	End Class
End Namespace

Imports Microsoft.VisualBasic
Imports System
Imports System.ComponentModel
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports DevExpress.ExpressApp
Imports DevExpress.Persistent.Base
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.Persistent.Validation

Imports System.Reflection

Namespace MainDemo.Module
	<DefaultClassOptions> _
	Public Class RuleRequiredFieldPersistent
        Inherits BaseObject
        Implements DevExpress.Persistent.Validation.IRuleSource
        Public Sub New(ByVal session As Session)
            MyBase.New(session)
        End Sub
        Public Property RuleName() As String
            Get
                Return GetPropertyValue(Of String)("RuleName")
            End Get
            Set(ByVal value As String)
                SetPropertyValue("RuleName", value)
            End Set
        End Property
        Public Property CustomMessageTemplate() As String
            Get
                Return GetPropertyValue(Of String)("CustomMessageTemplate")
            End Get
            Set(ByVal value As String)
                SetPropertyValue("CustomMessageTemplate", value)
            End Set
        End Property
        Public Property SkipNullOrEmptyValues() As Boolean
            Get
                Return GetPropertyValue(Of Boolean)("SkipNullOrEmptyValues")
            End Get
            Set(ByVal value As Boolean)
                SetPropertyValue("SkipNullOrEmptyValues", value)
            End Set
        End Property
        Public Property Id() As String
            Get
                Return GetPropertyValue(Of String)("Id")
            End Get
            Set(ByVal value As String)
                SetPropertyValue("Id", value)
            End Set
        End Property
        Public Property InvertResult() As Boolean
            Get
                Return GetPropertyValue(Of Boolean)("InvertResult")
            End Get
            Set(ByVal value As Boolean)
                SetPropertyValue("InvertResult", value)
            End Set
        End Property
        Public Property ContextIDs() As String
            Get
                Return GetPropertyValue(Of String)("ContextIDs")
            End Get
            Set(ByVal value As String)
                SetPropertyValue("ContextIDs", value)
            End Set
        End Property
        Public Property [Property]() As String
            Get
                Return GetPropertyValue(Of String)("Property")
            End Get
            Set(ByVal value As String)
                SetPropertyValue("Property", value)
            End Set
        End Property
        <Persistent("ObjectType")> _
        Protected Property ObjectType() As String
            Get
                If ObjectTypeCore IsNot Nothing Then
                    Return ObjectTypeCore.FullName
                End If
                Return ""
            End Get
            Set(ByVal value As String)
                ObjectTypeCore = ReflectionHelper.FindType(value)
            End Set
        End Property
        <NonPersistent(), TypeConverter(GetType(DevExpress.Persistent.Base.LocalizedClassInfoTypeConverter))> _
        Public Property ObjectTypeCore() As Type
            Get
                Return GetPropertyValue(Of Type)("ObjectTypeCore")
            End Get
            Set(ByVal value As Type)
                SetPropertyValue("ObjectTypeCore", value)
            End Set
        End Property
#Region "IRuleSource Members"
        Public Function CreateRules() As System.Collections.Generic.ICollection(Of DevExpress.Persistent.Validation.IRule) Implements DevExpress.Persistent.Validation.IRuleSource.CreateRules
            Dim list As New System.Collections.Generic.List(Of IRule)()
            Dim rule As New RuleRequiredField()
            rule.Properties.SkipNullOrEmptyValues = Me.SkipNullOrEmptyValues
            rule.Properties.Id = Me.Id
            rule.Properties.InvertResult = Me.InvertResult
            rule.Properties.CustomMessageTemplate = Me.CustomMessageTemplate
            rule.Properties.TargetContextIDs = New ContextIdentifiers(Me.ContextIDs).ToString()
            rule.Properties.TargetType = Me.ObjectTypeCore
            If rule.Properties.TargetType IsNot Nothing Then
                For Each pi As PropertyInfo In rule.Properties.TargetType.GetProperties()
                    If pi.Name = Me.Property Then
                        rule.Properties.TargetPropertyName = pi.Name
                    End If
                Next pi
            End If
            For i As Integer = Validator.RuleSet.RegisteredRules.Count - 1 To 0 Step -1
                If Validator.RuleSet.RegisteredRules(i).Id = Me.Id Then
                    Validator.RuleSet.RegisteredRules.RemoveAt(i)
                End If
            Next i
            list.Add(rule)
            Return list
        End Function
        <Browsable(False)> _
        Public ReadOnly Property Name() As String Implements DevExpress.Persistent.Validation.IRuleSource.Name
            Get
                Return Me.RuleName
            End Get
        End Property
#End Region
    End Class
End Namespace
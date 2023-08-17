#Region "Help:  Introduction to the script task"
'The Script Task allows you to perform virtually any operation that can be accomplished in
'a .Net application within the context of an Integration Services control flow. 

'Expand the other regions which have "Help" prefixes for examples of specific ways to use
'Integration Services features within this script task.
#End Region
#Region "Imports"

Imports System
Imports System.Data
Imports System.Math
Imports Microsoft.SqlServer.Dts.Runtime
Imports System.IO
Imports System.IO.Compression

#End Region

#Region "Imports"
Imports System
Imports System.Data
Imports System.Math
Imports Microsoft.SqlServer.Dts.Runtime
#End Region

'ScriptMain is the entry point class of the script.  Do not change the name, attributes,
'or parent of this class.
<Microsoft.SqlServer.Dts.Tasks.ScriptTask.SSISScriptTaskEntryPointAttribute()> _
<System.CLSCompliantAttribute(False)> _
Partial Public Class ScriptMain
    Inherits Microsoft.SqlServer.Dts.Tasks.ScriptTask.VSTARTScriptObjectModelBase

#Region "Help:  Using Integration Services variables and parameters in a script"
    'To use a variable in this script, first ensure that the variable has been added to 
    'either the list contained in the ReadOnlyVariables property or the list contained in 
    'the ReadWriteVariables property of this script task, according to whether or not your
    'code needs to write to the variable.  To add the variable, save this script, close this instance of
    'Visual Studio, and update the ReadOnlyVariables and 
    'ReadWriteVariables properties in the Script Transformation Editor window.
    'To use a parameter in this script, follow the same steps. Parameters are always read-only.

    'Example of reading from a variable:
    ' startTime = Dts.Variables("System::StartTime").Value

    'Example of writing to a variable:
    ' Dts.Variables("User::myStringVariable").Value = "new value"

    'Example of reading from a package parameter:
    ' batchId = Dts.Variables("$Package::batchId").Value

    'Example of reading from a project parameter:
    ' batchId = Dts.Variables("$Project::batchId").Value

    'Example of reading from a sensitive project parameter:
    ' batchId = Dts.Variables("$Project::batchId").GetSensitiveValue()
#End Region

#Region "Help:  Firing Integration Services events from a script"
    'This script task can fire events for logging purposes.

    'Example of firing an error event:
    ' Dts.Events.FireError(18, "Process Values", "Bad value", "", 0)

    'Example of firing an information event:
    ' Dts.Events.FireInformation(3, "Process Values", "Processing has started", "", 0, fireAgain)

    'Example of firing a warning event:
    ' Dts.Events.FireWarning(14, "Process Values", "No values received for input", "", 0)
#End Region

#Region "Help:  Using Integration Services connection managers in a script"
    'Some types of connection managers can be used in this script task.  See the topic 
    '"Working with Connection Managers Programatically" for details.

    'Example of using an ADO.Net connection manager:
    ' Dim rawConnection As Object = Dts.Connections("Sales DB").AcquireConnection(Dts.Transaction)
    ' Dim myADONETConnection As SqlConnection = CType(rawConnection, SqlConnection)
    ' <Use the connection in some code here, then release the connection>
    ' Dts.Connections("Sales DB").ReleaseConnection(rawConnection)

    'Example of using a File connection manager
    ' Dim rawConnection As Object = Dts.Connections("Prices.zip").AcquireConnection(Dts.Transaction)
    ' Dim filePath As String = CType(rawConnection, String)
    ' <Use the connection in some code here, then release the connection>
    ' Dts.Connections("Prices.zip").ReleaseConnection(rawConnection)
#End Region

    'This method is called when this script task executes in the control flow.
    'Before returning from this method, set the value of Dts.TaskResult to indicate success or failure.
    'To open Help, press F1.

    Public Sub Main()
        '
        ' Add your code here
        '
        Dim zipPath As String = CType(Dts.Variables("ZipPath").Value, String)
        Dim zipFilter As String = CType(Dts.Variables("ZipFilter").Value, String)
        Dim extractPath As String = CType(Dts.Variables("ExtractPath").Value, String)

        Dim zipFiles() As String
        Dim zipFile As String
        Dim fileChangeDt As Date
        Dim fileAge As TimeSpan
        Dim newestAge As New TimeSpan(3, 0, 0, 0)
        Dim newestFile As String
        Dim count As Integer = 0
        Dim filesFound As Boolean = False
        Dim startTime As Date = Now
        Dim timeOutValue As Integer = 1
        Dim shObj As Object = Activator.CreateInstance(Type.GetTypeFromProgID("Shell.Application"))



        Try
            Do
                If startTime.AddMinutes(timeOutValue) < Now Then
                    Throw New Exception("no file fitting the time criteria were found. Package timeout limit has been exceeded")

                    zipFiles = Directory.GetFiles(zipPath, zipFilter)

                    'Start for each loop that searches through the files for any that it the 3 day criteria set in variable newestAge.

                    For Each zipFile In zipFiles

                        'If a file has already been saved to an array then this will be set to true.
                        'This will allow the program to save time and skip the compare process until a file is available to compare.


                        'Compare the current file to the 3 day criteria to determine the file should be copied.
                        fileChangeDt = File.GetLastWriteTime(zipFile)
                        fileAge = DateTime.Now.Subtract(fileChangeDt)

                        If TimeSpan.op_LessThan(fileAge, newestAge) Then
                            newestFile = zipFile

                            'IO.Directory.CreateDirectory(extractPath)

                            'Declare the folder where the items will be extracted.
                            Dim output As Object = shObj.NameSpace((extractPath))

                            'Declare the input zip file.
                            Dim input As Object = shObj.NameSpace((zipFile))

                            'Extract the items from the zip file.
                            output.CopyHere((input.Items), 4)

                            filesFound = True


                        End If
                    Next

                    If filesFound = False Then

                        System.Threading.Thread.Sleep(60000)

                    End If
                End If
            Loop Until filesFound = True

            Dts.TaskResult = ScriptResults.Success

        Catch ex As Exception

            Dts.Events.FireError(-1, "", "Error in GetNewesFile - " + ex.Message, "", 0)

        End Try

    End Sub

#Region "ScriptResults declaration"
    'This enum provides a convenient shorthand within the scope of this class for setting the
    'result of the script.

    'This code was generated automatically.
    Enum ScriptResults
        Success = Microsoft.SqlServer.Dts.Runtime.DTSExecResult.Success
        Failure = Microsoft.SqlServer.Dts.Runtime.DTSExecResult.Failure
    End Enum

#End Region

End Class
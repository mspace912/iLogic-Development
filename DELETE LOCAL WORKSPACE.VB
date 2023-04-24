'Start of iLogic code
 
'Note to self -
'Remember to subscribe To Luke Davenport's blog and follow him on Twitter @LukeCadline
 
Imports System
Imports System.IO
Imports System.Text
 
Sub Main
 
oDoc = ThisDoc.Document
 
' Note - enter your Vault Workspace local folder location here.....
Dim SearchLocation As String = "C:\VAULT WORKSPACE\Designs"
 
Dim filelist As New List(Of String)
 
Call GetAllReadOnlyFiles(SearchLocation, filelist)
 
If filelist.count > 0 Then
    i = MessageBox.Show(filelist.count & " Read-Only file(s) found - Delete Them?" & vbLf & vbLf & _
    "Note: Please use this code with extreme caution - Cadline cannot be held responsible for files deleted in error" & vbLf & vbLf & _
    "Note: It cannot be guaranteed that read-only files are actually checked into Vault and exist in the Vault database - only proceed if you are sure...", _
    "Cadline iLogic",MessageBoxButtons.YesNo,MessageBoxIcon.Question) 
    If i = vbNo Then
           Return
    End If
Else
    MessageBox.Show("No Read-Only files were found in selected folder - Exiting Rule...", "Cadline iLogic")
    Return
End If
 
For Each strfile As String In filelist
    System.IO.File.SetAttributes(strfile, IO.FileAttributes.Normal)
    System.IO.File.Delete(strfile)
Next
 
End Sub
 
 Sub GetAllReadOnlyFiles(ByVal SearchLocation As String, ByRef filelist As List(Of String))
    
        For Each file As String In Directory.GetFiles(SearchLocation)
            Dim attributes = IO.File.GetAttributes(file)
               If ((attributes And FileAttributes.ReadOnly) = FileAttributes.ReadOnly) Then    
                filelist.add(file)
            End If
        Next
        
        For Each dir As String In Directory.GetDirectories(SearchLocation)
            If Not dir.Contains("_V") Then
                Try
                    GetAllReadOnlyFiles(dir, filelist)
                Catch
                End Try
            Else
            End If
        Next
        
 End Sub
 
 ' End of iLogic code...............
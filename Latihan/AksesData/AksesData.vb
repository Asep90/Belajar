Imports MySql.Data.MySqlClient
Namespace AksesData
    Public Class DataControl
        Private myconnection As New AksesData.koneksiDB
        Public Function GetdataSet(ByVal SQL As String) As DataSet
            Dim adapter As New MySqlDataAdapter(SQL, myconnection.open)
            Dim myData As New DataSet
            adapter.Fill(myData, "Data")
            Return myData
        End Function
    End Class
End Namespace


Imports MySql.Data.MySqlClient
Public Class FormPegawai
    Public strSql As String
    Public DB As MySqlConnection
    Public CMD As MySqlCommand
    Public ADP As MySqlDataAdapter
    Public DR As MySqlDataReader
    Public DS As New DataSet
    Public Simpan, Hapus As String
    Sub Tambah()
        DataGridView1.Enabled = False
        txtAlamat_Pegawai.Clear()
        txtAlamat_Pegawai.Clear()
        txtTelepon_Pegawai.Clear()
        txtNama_Pegawai.Enabled = True
        txtAlamat_Pegawai.Enabled = True
        txtTelepon_Pegawai.Enabled = True
        txtNama_Pegawai.Focus()
        btnTambah.Enabled = False
        btnSimpan.Enabled = True
        btnHapus.Enabled = True
        btnBatal.Enabled = False
        btnKeluar.Enabled = False
        btnUbah.Enabled = True
    End Sub
    Sub Ubah()
        txtNama_Pegawai.Enabled = False
        txtAlamat_Pegawai.Enabled = True
        txtTelepon_Pegawai.Enabled = True
        txtNama_Pegawai.Focus()
        btnTambah.Enabled = True
        btnSimpan.Enabled = False
        btnHapus.Enabled = True
        btnBatal.Enabled = True
        btnKeluar.Enabled = True
        btnUbah.Enabled = True
    End Sub
    Sub Batal()
        DataGridView1.Enabled = True
        txtAlamat_Pegawai.Clear()
        txtAlamat_Pegawai.Clear()
        txtTelepon_Pegawai.Clear()
        txtNama_Pegawai.Enabled = False
        txtAlamat_Pegawai.Enabled = False
        txtTelepon_Pegawai.Enabled = False
        btnTambah.Enabled = False
        btnSimpan.Enabled = True
        btnHapus.Enabled = True
        btnBatal.Enabled = False
        btnKeluar.Enabled = False
        btnUbah.Enabled = True
    End Sub
    Sub Bersih()
        txtAlamat_Pegawai.Clear()
        txtAlamat_Pegawai.Clear()
        txtTelepon_Pegawai.Clear()
        txtNama_Pegawai.Focus()
    End Sub
    Sub IsiGrid()
        Modulekoneksi.bukaDB()
        DA = New MySqlDataAdapter("SELECT * FROM Pegawai", Koneksi)
        DS = New DataSet
        DA.Fill(DS, "pegawai")
        DataGridView1.DataSource = DS.Tables("pegawai")
        DataGridView1.ReadOnly = True
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        Call bukaDB()
        CMD = New MySqlCommand("SELECT Kode_Pasien from Pasien Where Kode_pasien ='" & txtNama_Pegawai.Text & "'", Koneksi)
        RD = CMD.ExecuteReader
        RD.Read()
        If btnTambah.Enabled = False Then
            If RD.HasRows Then
                MsgBox("Maaf, Data dengan Nama Kode_Pasien tersebut telah ada", MsgBoxStyle.Exclamation, "Peringatan")
            Else
                If MessageBox.Show("Simpan data baru? ", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    Call bukaDB()
                    Simpan = "INSERT INTO Pegawai (Nama_Pegawai,Alamat_pegawai,Telepon_Pegawai) VALUES ('" & txtNama_Pegawai.Text & "','" & txtAlamat_Pegawai.Text & "','" & txtTelepon_Pegawai.Text & "')"
                    CMD = New MySqlCommand(Simpan, Koneksi)
                    CMD.ExecuteNonQuery()
                    Call IsiGrid()
                    Call Batal()
                    txtNama_Pegawai.Focus()
                    MsgBox("Data Tersimpan", MsgBoxStyle.Information, "Information")
                    RD.Close()
                End If
            End If
        End If
    End Sub

    Private Sub btnTambah_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTambah.Click
        Call Tambah()
    End Sub

    Private Sub btnUbah_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUbah.Click
        bukaDB()
        Try
            Using conn As New MySqlConnection(Koneksi.ConnectionString)
                conn.Open()
                Dim Command As New MySqlCommand("update pegawai set Nama_Pegawai=@Nama_Pegawai,Alamat_Pegawai=@Alamat_Pegawai,Telepon_Pegawai=@Telepon_Pegawai where Nama_pegawai=@Nama_Pegawai", conn)
                With Command.Parameters
                    .AddWithValue("@Nama_Pegawai", txtNama_Pegawai.Text)
                    .AddWithValue("@Alamat_Pegawai", txtAlamat_Pegawai.Text)
                    .AddWithValue("@Telepon_Pegawai", txtTelepon_Pegawai.Text)
                End With
                Command.ExecuteNonQuery()
                MessageBox.Show("Data Sukses Tersimpan", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message, "kesalahan Dalam pengimputan Data", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Call IsiGrid()
        Call Bersih()
    End Sub

    Private Sub btnHapus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHapus.Click
        If txtNama_Pegawai.Text = "" Then
            MessageBox.Show("Pilih Dahulu Data Yang Akan diHapus pada tabel diatas!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            If txtNama_Pegawai.Text <> "" Then
                If MessageBox.Show("Apakah Anda Yakin akan Menghahapus data tabel diatas!", "konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    Call bukaDB()
                    Hapus = "DELETE From pegawai where Nama_Pegawai='" & txtNama_Pegawai.Text & "'"
                    CMD = New MySqlCommand(Hapus, Koneksi)
                    CMD.ExecuteNonQuery()
                    Call IsiGrid()
                    Call Bersih()
                    txtNama_Pegawai.Focus()
                    MessageBox.Show("Data Berhasil dihapus", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)

                End If
            End If
        End If
    End Sub

    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        Call Batal()
    End Sub

    Private Sub btnKeluar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKeluar.Click
        Me.Hide()
        FormUtama.Show()

    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        Call bukaDB()
        CMD = New MySqlCommand("SELECT Nama_Pegawai,Alamat_Pegawai,Telepon_Pegawai FROM WHERE Kode_Pasien='" & txtNama_Pegawai.Text & "'", DB)
        Try
            Dim gridbaris As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
            txtNama_Pegawai.Text = gridbaris.Cells(0).Value.ToString
            txtAlamat_Pegawai.Text = gridbaris.Cells(1).Value.ToString
            txtTelepon_Pegawai.Text = gridbaris.Cells(2).Value.ToString
        Catch ex As Exception
            MsgBox("Pilih yang didalam Tabel", "MsgBoxStyle.Information, INFORMASI")

        End Try
    End Sub

    Private Sub FormPasien_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call IsiGrid()
        Call Ubah()
    End Sub
End Class

   
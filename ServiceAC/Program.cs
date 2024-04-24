using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace insert_read
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connStr = "Data Source=LAPTOP-C7BGE8NV\\WAHYUIT; Initial Catalog=Service_AC; User ID=sa; Password=222";
            SqlConnection conn = new SqlConnection(connStr);

            Program pr = new Program();
            while (true)
            {
                try
                {
                    Console.Write("\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                    Console.Write("\n|                                 S E L A M A T  D A T A N G                                 |");
                    Console.Write("\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                    Console.Write("\n\nKetik K untuk Terhubung ke Database atau E untuk keluar dari Aplikasi" +
                        "\n: ");
                    char chr = Convert.ToChar(Console.ReadLine());
                    switch (chr)
                    {
                        case 'K':
                            {
                                Console.Clear();
                                Console.Write("\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                                Console.Write("\n|                                 L O G I N                                 |");
                                Console.Write("\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                                Console.WriteLine("\n\nMasukkan username:");
                                string username = Console.ReadLine();

                                Console.WriteLine("Masukkan password:");
                                string password = Console.ReadLine();

                                if (ValidateLogin(username, password, conn))
                                {
                                    Console.WriteLine("Login berhasil!");
                                    while (true)
                                    {
                                        try
                                        {
                                            Console.WriteLine("\nSERVICE AC");
                                            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                                            Console.WriteLine("|  MENU                                           |");                
                                            Console.WriteLine("|  1. Melihat Data Layanan Service AC             |");
                                            Console.WriteLine("|  2. Menambah Data Layanan Service AC            |");
                                            Console.WriteLine("|  3. Menghapus Data                              |");
                                            Console.WriteLine("|  4. Mengubah Data                               |");
                                            Console.WriteLine("|  5. Mencari Data                                |");
                                            Console.WriteLine("|  6. Keluar                                      |");
                                            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                                            Console.WriteLine("\nEDIT ADMIN");
                                            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                                            Console.WriteLine("|  7. Melihat Data Admin                          |");
                                            Console.WriteLine("|  8. Menambah Data Untuk Admin Baru              |");
                                            Console.WriteLine("|  9. Menghapus Data Admin                        |");
                                            Console.WriteLine("|  0. Mengubah Data Admin                         |");
                                            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                                            Console.Write("\nEnter your choice (0-9): ");
                                            char choice = Convert.ToChar(Console.ReadLine());
                                            switch (choice)
                                            {
                                                case '1':
                                                    {
                                                        Console.Clear();
                                                        Console.WriteLine("Data Layanan Service AC\n");
                                                        pr.ReadDataLayanan(conn);
                                                    }
                                                    break;
                                                case '2':
                                                    {
                                                        bool dataValid = false;
                                                        do
                                                        {
                                                            Console.Clear();
                                                            Console.WriteLine("Input Data Layanan Service AC\n");
                                                            Console.WriteLine("Masukkan Nama Pelanggan  :");
                                                            string NmPelanggan = Console.ReadLine();
                                                            Console.WriteLine("Masukkan Tanggal Layanan (yyyy-MM-dd) :");
                                                            string TglLayanan = Console.ReadLine();
                                                            Console.WriteLine("Masukkan Biaya Layanan   :");
                                                            string BiayaLayanan = Console.ReadLine();
                                                            Console.WriteLine("Masukkan Status Layanan  : ");
                                                            string StsLayanan = Console.ReadLine();
                                                            try
                                                            {
                                                                dataValid = pr.InsertData(NmPelanggan, TglLayanan, BiayaLayanan, StsLayanan, conn);
                                                            }
                                                            catch (Exception e)
                                                            {
                                                                Console.WriteLine("\nAnda tidak memiliki " +
                                                                    "akses untuk menambah data atau Data yang anda masukkan salah");
                                                                Console.WriteLine(e.ToString());
                                                            }

                                                        } while (!dataValid);
                                                    }
                                                    break;
                                                case '3':
                                                    {
                                                        Console.Clear();
                                                        bool hapusValid = false;

                                                        do
                                                        {
                                                            try
                                                            {
                                                                hapusValid = pr.DeleteData(conn);
                                                            }
                                                            catch (Exception e)
                                                            {
                                                                Console.WriteLine("\nAnda tidak memiliki " +
                                                                    "akses untuk menghapus data atau Data yang anda masukkan salah");
                                                                Console.WriteLine(e.ToString());
                                                            }

                                                        } while (!hapusValid);
                                                    }
                                                    break;
                                                case '4':
                                                    {
                                                        bool updateValid = false;
                                                        do
                                                        {
                                                            Console.Clear();
                                                            Console.WriteLine("Update Data Layanan Service AC\n");

                                                            pr.ReadDataLayanan(conn);
                                                            conn.Close(); 

                                                            Console.WriteLine("Masukkan Nama Pelanggan yang ingin diubah: ");
                                                            string oldNmPelanggan = Console.ReadLine();

                                                            if (!pr.CheckIfPelangganExists(oldNmPelanggan, conn))
                                                            {
                                                                Console.WriteLine("Nama Pelanggan tidak ditemukan dalam database.");
                                                                break;

                                                            }

                                                            Console.WriteLine("Masukkan Nama Pelanggan baru                 : ");
                                                            string NmPelanggan = Console.ReadLine();
                                                            Console.WriteLine("Masukkan Tanggal Layanan baru (yyyy-MM-dd)   : ");
                                                            string TglLayanan = Console.ReadLine();
                                                            Console.WriteLine("Masukkan Biaya Layanan baru                  : ");
                                                            string BiayaLayanan = Console.ReadLine();
                                                            Console.WriteLine("Masukkan Status Layanan baru                 : ");
                                                            string StsLayanan = Console.ReadLine();

                                                            Console.WriteLine("\nApakah Anda yakin ingin mengubah data ini? (Y/N):");
                                                            string confirmation = Console.ReadLine();

                                                            if (confirmation.ToUpper() == "Y")
                                                            {
                                                                try
                                                                {
                                                                    updateValid = pr.UpdateData(oldNmPelanggan, NmPelanggan, TglLayanan, BiayaLayanan, StsLayanan, conn);
                                                                }
                                                                catch (Exception e)
                                                                {
                                                                    Console.WriteLine("\nTerjadi kesalahan saat mengubah data.");
                                                                    Console.WriteLine(e.ToString());
                                                                }
                                                            }
                                                            else if (confirmation.ToUpper() == "N")
                                                            {
                                                                Console.WriteLine("Pengubahan data dibatalkan.");
                                                                updateValid = false;
                                                                conn.Open();
                                                            }
                                                            else
                                                            {
                                                                Console.WriteLine("Pilihan tidak valid. Pengubahan data dibatalkan.");
                                                                updateValid = false;
                                                                conn.Open();
                                                            }
                                                        } while (!updateValid);
                                                    }
                                                    break;
                                                case '5':
                                                    {
                                                        Console.Clear();
                                                        Console.WriteLine("Cari Data Layanan Service AC\n");
                                                        pr.UntukMencari(conn);
                                                        conn.Close();

                                                        Console.WriteLine("Masukkan Nama Pelanggan yang ingin Anda cari:");
                                                        string searchName = Console.ReadLine();

                                                        Console.WriteLine("\nHasil Pencarian:");
                                                        if (!string.IsNullOrWhiteSpace(searchName))
                                                        {
                                                            try
                                                            {
                                                                pr.SearchLayananServiceAC(searchName, conn);
                                                            }
                                                            catch (Exception ex)
                                                            {
                                                                Console.WriteLine("Terjadi kesalahan saat mencari data: " + ex.Message);
                                                            }
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine("Nama pelanggan tidak boleh kosong.");
                                                        }
                                                    }
                                                    break;

                                                case '6':
                                                    {
                                                        Console.WriteLine("\nKeluar dari aplikasi.");
                                                        Environment.Exit(0);
                                                    }
                                                    break;
                                                default:
                                                    {
                                                        Console.Clear();
                                                        Console.WriteLine("\nInvalid option");
                                                    }
                                                    break;
                                                case '7':
                                                    {
                                                        Console.Clear();
                                                        Console.WriteLine("Data Admin\n");
                                                        pr.ReadDataAdmin(conn);
                                                    }
                                                    break;
                                                case '8':
                                                    {
                                                        bool dataValid = false;
                                                        do
                                                        {
                                                            Console.Clear();
                                                            Console.WriteLine("Input Data Untuk Menambah Admin\n");
                                                            Console.WriteLine("Masukkan Nama Admin                      :");
                                                            string NmAdmin = Console.ReadLine();
                                                            Console.WriteLine("Masukkan Username (Exampel@gmail.com)    :");
                                                            string Username = Console.ReadLine();
                                                            Console.WriteLine("Masukkan Password                        :");
                                                            string Password = Console.ReadLine();
                                                            try
                                                            {
                                                                dataValid = pr.InsertDataAdmin(NmAdmin, Username, Password, conn);
                                                            }
                                                            catch (Exception e)
                                                            {
                                                                Console.WriteLine("\nAnda tidak memiliki " +
                                                                    "akses untuk menambah data atau Data yang anda masukkan salah");
                                                                Console.WriteLine(e.ToString());
                                                            }

                                                        } while (!dataValid);
                                                    }
                                                    break;
                                                case '9':
                                                    {
                                                        Console.Clear();
                                                        bool hapusValid = false;

                                                        do
                                                        {
                                                            try
                                                            {
                                                                hapusValid = pr.DeleteDataAdmin(conn);
                                                            }
                                                            catch (Exception e)
                                                            {
                                                                Console.WriteLine("\nAnda tidak memiliki " +
                                                                    "akses untuk menghapus data atau Data yang anda masukkan salah");
                                                                Console.WriteLine(e.ToString());
                                                            }

                                                        } while (!hapusValid);
                                                    }
                                                    break;
                                                case '0':
                                                    {
                                                        bool updateValid = false;
                                                        do
                                                        {
                                                            Console.Clear();
                                                            Console.WriteLine("Update Data Admin\n");

                                                            pr.ReadDataAdmin(conn);
                                                            conn.Close();

                                                            Console.WriteLine("Masukkan Nama Admin yang ingin diubah: ");
                                                            string oldNmAdmin = Console.ReadLine();

                                                            if (!pr.CheckIfAdminExists(oldNmAdmin, conn))
                                                            {
                                                                Console.WriteLine("Nama Admin tidak ditemukan dalam database.");
                                                                break;

                                                            }

                                                            Console.WriteLine("Masukkan Nama Admin baru                     : ");
                                                            string NmAdmin = Console.ReadLine();
                                                            Console.WriteLine("Masukkan Username baru (Exampel@gmail.com)   : ");
                                                            string Username = Console.ReadLine();
                                                            Console.WriteLine("Masukkan Password baru                       : ");
                                                            string Password = Console.ReadLine();

                                                            Console.WriteLine("\nApakah Anda yakin ingin mengubah data ini? (Y/N):");
                                                            string confirmation = Console.ReadLine();

                                                            if (confirmation.ToUpper() == "Y")
                                                            {
                                                                try
                                                                {
                                                                    updateValid = pr.UpdateDataAdmin(oldNmAdmin, NmAdmin, Username, Password, conn);
                                                                }
                                                                catch (Exception e)
                                                                {
                                                                    Console.WriteLine("\nTerjadi kesalahan saat mengubah data.");
                                                                    Console.WriteLine(e.ToString());
                                                                }
                                                            }
                                                            else if (confirmation.ToUpper() == "N")
                                                            {
                                                                Console.WriteLine("Pengubahan data dibatalkan.");
                                                                updateValid = false;
                                                                conn.Open();
                                                            }
                                                            else
                                                            {
                                                                Console.WriteLine("Pilihan tidak valid. Pengubahan data dibatalkan.");
                                                                updateValid = false;
                                                                conn.Open();
                                                            }
                                                        } while (!updateValid);
                                                    }
                                                    break;
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine("Terjadi kesalahan: " + ex.Message);
                                        }
                                    }

                                }
                                else
                                {
                                    Console.WriteLine("Username atau password salah. Silakan coba lagi.");
                                }
                            }
                            break;

                        case 'E':
                            Environment.Exit(0);
                            break;

                        default:
                            Console.WriteLine("Input tidak valid. Ketik 'K' untuk terhubung atau 'E' untuk keluar.");
                            break;
                    }
                }
                catch
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Tidak Dapat Mengakses Database Tersebut\n");
                    Console.ResetColor();
                }
            }
        }

        static bool ValidateLogin(string username, string password, SqlConnection conn)
        {
            string hashedPassword = (password);

            string query = $"SELECT COUNT(*) FROM Admin WHERE Username='{username}' AND Password='{hashedPassword}'";

            int count = 0;
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                count = (int)cmd.ExecuteScalar();
            }

            return count > 0;
        }


        public void ReadDataLayanan(SqlConnection con)
        {
            SqlCommand cmd = new SqlCommand("SELECT NmPelanggan, TglLayanan, BiayaLayanan, StsLayanan FROM LayananServiceAC", con);
            SqlDataReader r = cmd.ExecuteReader();
            while (r.Read())
            {
                for (int i = 0; i < r.FieldCount; i++)
                {
                    Console.WriteLine(r.GetValue(i));
                }
                Console.WriteLine();
            }
            r.Close();
        }

        public void ReadDataAdmin(SqlConnection con)
        {
            SqlCommand cmd = new SqlCommand("SELECT NmAdmin, Username, Password FROM Admin", con);
            SqlDataReader r = cmd.ExecuteReader();
            while (r.Read())
            {
                for (int i = 0; i < r.FieldCount; i++)
                {
                    Console.WriteLine(r.GetValue(i));
                }
                Console.WriteLine();
            }
            r.Close();
        }

        public bool InsertData(string NmPelanggan, string TglLayanan, string BiayaLayanan, string StsLayanan, SqlConnection con)
        {
            Console.WriteLine("Data yang ingin dimasukkan:");

            Console.WriteLine($"Nama Pelanggan  : {NmPelanggan}");
            Console.WriteLine($"Tanggal Layanan : {TglLayanan}");
            Console.WriteLine($"Biaya Layanan   : {BiayaLayanan}");
            Console.WriteLine($"Status Layanan  : {StsLayanan}");

            Console.Write("Apakah data yang dimasukkan benar? (Y/N): ");
            string confirmation = Console.ReadLine();

            if (confirmation.ToUpper() == "Y")
            {
                string str = " ";
                str = "INSERT INTO LayananServiceAC (NmPelanggan, TglLayanan, BiayaLayanan, StsLayanan) " +
                    "VALUES (@np, @tl, @bl, @sl)";
                using (SqlCommand cmd = new SqlCommand(str, con))
                {
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.Add(new SqlParameter("np", NmPelanggan));
                    cmd.Parameters.Add(new SqlParameter("tl", DateTime.ParseExact(TglLayanan, "yyyy-MM-dd", null)));
                    cmd.Parameters.Add(new SqlParameter("bl", BiayaLayanan));
                    cmd.Parameters.Add(new SqlParameter("sl", StsLayanan));
                    cmd.ExecuteNonQuery();
                }
                Console.WriteLine("Data Berhasil Ditambahkan");
                return true;
            }
            else if (confirmation.ToUpper() == "N")
            {
                Console.WriteLine("Silakan isi ulang data.");
                return false;
            }
            else
            {
                Console.WriteLine("Pilihan tidak valid. Silakan coba lagi.");
                return false;
            }
        }

        public bool DeleteData(SqlConnection con)
        {
            bool penghapusanBerhasil = false; 

            while (true)
            {
                Console.WriteLine("Data Layanan Service AC sebelum penghapusan:\n");
                ReadDataLayanan(con);

                Console.WriteLine("\nMasukkan Nama Pelanggan yang ingin dihapus:");
                string NmPelanggan = Console.ReadLine();

                Console.WriteLine($"\nApakah Anda yakin ingin menghapus data untuk {NmPelanggan}? (Y/N):");
                string confirmation = Console.ReadLine();

                if (confirmation.ToUpper() == "Y")
                {
                    string str = "DELETE FROM LayananServiceAC WHERE NmPelanggan = @np";
                    using (SqlCommand cmd = new SqlCommand(str, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("np", NmPelanggan));
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Data berhasil dihapus.");
                            penghapusanBerhasil = true;
                        }
                        else
                        {
                            Console.WriteLine("Tidak ada data yang dihapus.");
                        }
                    }
                    break; 
                }
                else if (confirmation.ToUpper() == "N")
                {
                    Console.WriteLine("Penghapusan dibatalkan.");
                    break;
                }
                else
                {
                    Console.WriteLine("Pilihan tidak valid. Silakan coba lagi.");
                }
            }

            return penghapusanBerhasil;
        }

        public bool UpdateData(string oldNmPelanggan, string NmPelanggan, string TglLayanan, string BiayaLayanan, string StsLayanan, SqlConnection con)
        {
            string query = "UPDATE LayananServiceAC SET NmPelanggan = @newNmPelanggan, TglLayanan = @TglLayanan, BiayaLayanan = @BiayaLayanan, StsLayanan = @StsLayanan WHERE NmPelanggan = @oldNmPelanggan";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@newNmPelanggan", NmPelanggan);
                cmd.Parameters.AddWithValue("@TglLayanan", DateTime.ParseExact(TglLayanan, "yyyy-MM-dd", null));
                cmd.Parameters.AddWithValue("@BiayaLayanan", BiayaLayanan);
                cmd.Parameters.AddWithValue("@StsLayanan", StsLayanan);
                cmd.Parameters.AddWithValue("@oldNmPelanggan", oldNmPelanggan);

                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                con.Close();

                if (rowsAffected > 0)
                {
                    Console.WriteLine("Data berhasil diubah.");
                    con.Open();
                    return true;
                    
                }
                else
                {
                    Console.WriteLine("Tidak ada data yang diubah.");
                    con.Open();
                    return true;
                    
                }
                
            }
        }

        public bool CheckIfPelangganExists(string NmPelanggan, SqlConnection con)
        {
            string query = "SELECT COUNT(*) FROM LayananServiceAC WHERE NmPelanggan = @NmPelanggan";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@NmPelanggan", NmPelanggan);
                con.Open();
                int count = (int)cmd.ExecuteScalar();
                con.Close();
                return count > 0;
            }
        }

        public void UntukMencari(SqlConnection con)
        {
            SqlCommand cmd = new SqlCommand("SELECT NmPelanggan FROM LayananServiceAC", con);
            SqlDataReader r = cmd.ExecuteReader();
            while (r.Read())
            {
                for (int i = 0; i < r.FieldCount; i++)
                {
                    Console.WriteLine(r.GetValue(i));
                }
                Console.WriteLine();
            }
            r.Close();
        }
        public void SearchLayananServiceAC(string searchName, SqlConnection con)
        {
            try
            {
                string query = "SELECT NmPelanggan, TglLayanan, BiayaLayanan, StsLayanan FROM LayananServiceAC WHERE NmPelanggan LIKE @searchName";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@searchName", "%" + searchName + "%");
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"Nama Pelanggan: {reader["NmPelanggan"]}");
                            Console.WriteLine($"Tanggal Layanan: {reader["TglLayanan"]}");
                            Console.WriteLine($"Biaya Layanan: {reader["BiayaLayanan"]}");
                            Console.WriteLine($"Status Layanan: {reader["StsLayanan"]}");
                            Console.WriteLine();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Data tidak ditemukan.");
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Terjadi kesalahan saat mencari data: {ex.Message}");
            }
        }

        public bool InsertDataAdmin(string NmAdmin, string Username, string Password, SqlConnection con)
        {
            Console.WriteLine("Data yang ingin dimasukkan:");

            Console.WriteLine($"Nama Admin  : {NmAdmin}");
            Console.WriteLine($"Username    : {Username}");
            Console.WriteLine($"Password    : {Password}");

            Console.Write("Apakah data yang dimasukkan benar? (Y/N): ");
            string confirmation = Console.ReadLine();

            if (confirmation.ToUpper() == "Y")
            {
                string str = " ";
                str = "INSERT INTO Admin (NmAdmin, Username, Password) " +
                    "VALUES (@Nad, @Usr, @Pas)";
                using (SqlCommand cmd = new SqlCommand(str, con))
                {
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.Add(new SqlParameter("Nad", NmAdmin));
                    cmd.Parameters.Add(new SqlParameter("Usr", Username));
                    cmd.Parameters.Add(new SqlParameter("Pas", Password));
                    cmd.ExecuteNonQuery();
                }
                Console.WriteLine("Data Berhasil Ditambahkan");
                return true;
            }
            else if (confirmation.ToUpper() == "N")
            {
                Console.WriteLine("Silakan isi ulang data.");
                return false;
            }
            else
            {
                Console.WriteLine("Pilihan tidak valid. Silakan coba lagi.");
                return false;
            }
        }

        public bool DeleteDataAdmin(SqlConnection con)
        {
            bool penghapusanBerhasil = false;

            while (true)
            {
                Console.WriteLine("Data Admin sebelum penghapusan:\n");
                ReadDataAdmin(con);

                Console.WriteLine("\nMasukkan Nama Admin yang ingin dihapus:");
                string NmAdmin = Console.ReadLine();

                Console.WriteLine($"\nApakah Anda yakin ingin menghapus data untuk {NmAdmin}? (Y/N):");
                string confirmation = Console.ReadLine();

                if (confirmation.ToUpper() == "Y")
                {
                    string str = "DELETE FROM Admin WHERE NmAdmin = @Nad";
                    using (SqlCommand cmd = new SqlCommand(str, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("Nad", NmAdmin));
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Data berhasil dihapus.");
                            penghapusanBerhasil = true;
                        }
                        else
                        {
                            Console.WriteLine("Tidak ada data yang dihapus.");
                        }
                    }
                    break;
                }
                else if (confirmation.ToUpper() == "N")
                {
                    Console.WriteLine("Penghapusan dibatalkan.");
                    break;
                }
                else
                {
                    Console.WriteLine("Pilihan tidak valid. Silakan coba lagi.");
                }
            }

            return penghapusanBerhasil;
        }

        public bool UpdateDataAdmin(string oldNmAdmin, string NmAdmin, string Username, string Password, SqlConnection con)
        {
            string query = "UPDATE Admin SET NmAdmin = @newNmAdmin, Username = @Username, Password = @Password WHERE NmAdmin = @oldNmAdmin";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@newNmAdmin", NmAdmin);
                cmd.Parameters.AddWithValue("@Username", Username);
                cmd.Parameters.AddWithValue("@Password", Password);
                cmd.Parameters.AddWithValue("@oldNmAdmin", oldNmAdmin);

                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                con.Close();

                if (rowsAffected > 0)
                {
                    Console.WriteLine("Data berhasil diubah.");
                    con.Open();
                    return true;

                }
                else
                {
                    Console.WriteLine("Tidak ada data yang diubah.");
                    con.Open();
                    return true;

                }

            }
        }
        public bool CheckIfAdminExists(string NmAdmin, SqlConnection con)
        {
            string query = "SELECT COUNT(*) FROM Admin WHERE NmAdmin = @NmAdmin";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@NmAdmin", NmAdmin);
                con.Open();
                int count = (int)cmd.ExecuteScalar();
                con.Close();
                return count > 0;
            }
        }

    }
}
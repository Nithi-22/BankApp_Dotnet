// See https://aka.ms/new-console-template for more information
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Banks{
  public class Program{
    
      static string connectionDB=@"server=localhost;userid=root;password=nithi22jiji@;database=db;";
      static MySqlConnection connection=new MySqlConnection(connectionDB);
      static void Main(string[]args){
        Bank bank=new Bank();
         bank.Welcome();
        Customer customer=new Customer();
         
          string? val=Console.ReadLine();
          string pin;
          if(val=="login" ){
            pin=customer.login();
            }
          else {
                 customer.createAccount();
                }
          
          
          
            


            
    }
}
            
}
    



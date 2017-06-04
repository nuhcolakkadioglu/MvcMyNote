# MvcMyNote
asp.net mvc ve mssql server ile çok katmanlı code first mimaride geliştirimiştir.

projeyi indirip direk kullanabilirsiniz,
Yapmanız gereken MyNote.Web katmanında bulunan web.config içindeki sql server bilgilerini 
kendinize uygun doldurmanız

birden çok veri tabanı destegi sağlayabilirsiniz,
mevcutta 2 adet mysql ve mssql server için yazılmış context bulunuyor.


veri tabanı tercihi yaomak için MyNote.BussinessLayer altındaki 
ManagerBase.cs dosyasındaki 8 satırda bulunan using MyNote.DataAccessLayer.MySql kodunu bu şekilde kullanırsanız mysql 
using MyNote.DataAccessLayer.EntityFramework; bu şekilde kullanırsanız MSSQL server kullanır.

web.config den connectionsring  ayarlarını yapmayı unutmayın .

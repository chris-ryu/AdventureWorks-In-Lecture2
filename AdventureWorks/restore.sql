RESTORE DATABASE AdventureWorks FROM DISK='C:\Users\wtime\Downloads\AdventureWorksLT2019.bak'
WITH MOVE 'AdventureWorksLT2012_Data' TO 'C:\Users\wtime\Downloads\AdventureWorksLT2019.mdf',
MOVE 'AdventureWorksLT2012_Log' TO 'C:\Users\wtime\Downloads\AdventureWorksLT2019.ldf'
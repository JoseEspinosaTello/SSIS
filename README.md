# SSIS
## Overview
$ellBy is interested in participating in an Amazon Vine program that offers incentives for users who submit Amazon Vine reviews of any $ellBy products. Big Market has been hired by $ellBy to determine if the program is worth the cost. The purpose of this project is to download a video game review dataset, use PySpark to perform the ETL process, upload the data to an AWS RDS instance in PostgreSQL, and perform an analysis in order to determine if there is any bias towards $ellBy products from paid Vine members, based on their reviews.  
## Resources


Applications/Technologies:

-	ETL (Extract, Transform, Load)
-	SSIS (SQL Server Integration Services)
-	VB.Net
-	Visual Studio 2022
-	Visual Studio Business Inteligence/Data Tools
-	C#
-	SQL Server Managment Studio
-	Transact SQL
-	Database Design

#Summary

The SSIS packages is designed to automate the ETL process and consist of 5 script tasks and 6 dataflow tasks. The script tasks extract and copy files to the folders used by the dataflow tasks. The script tasks also maintain file integrity by ensuring there are clean files where needed and files are not overwritten. The data flow tasks are broken up into two groups; Excel source dataflow tasks and OLEDB dataflow tasks. the Excel source dataflow tasks upload data to the database tables and the OLEDB dataflow task generate an Excel sheet summary of the uploaded data to confirm the ETL process has completed.


![package](https://github.com/JoseEspinosaTello/SSIS/blob/main/Snaps/SSISPackage.png)

#Script tasks

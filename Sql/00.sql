USE [master]
GO

CREATE DATABASE [SrGroceryList]  
ON   
( NAME = N'SrGroceryList',  
    FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\SrGroceryList.mdf',  
    SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )  
LOG ON  
( NAME = 'SrGroceryList_log',  
    FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\SrGroceryList_log.ldf',  
    SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB ) ;  
GO  


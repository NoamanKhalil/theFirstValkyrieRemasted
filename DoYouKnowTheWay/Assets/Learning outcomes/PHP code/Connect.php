<?php
// script Used to Register/Insert Data

//------------------------------------//
//echo "Connect Running";
//server login settings 
$servername = "localhost";
$dbUser= "id4796405_admin";
$dbPassword = "00000";
$dbname = "id4796405_thefirstvalkyrie";
//------------------------------------//
$username =$_POST ["usernamePost"]; // CARLOSISBAE;
$Email =$_POST ["emailPost"]  ;  //"CARLOSISBAE@YAHOO.com";
$Password=$_POST ["passwordPost"]; // "12345";

$conn = mysqli_connect ($servername,$dbUser,$dbPassword,$dbname);

// check connection 
if (!$conn)
{
	die ("Connection@@@@FAILED" . mysqli_error());
}
$doesEmailExist = False;
$doesUserExist  =False;

$sqli = "SELECT  Email FROM login  WHERE Email = '".$Email."'";


$result = mysqli_query ($conn , $sqli);

if (mysqli_num_rows ($result) > 0)
{
	// show data for each row
	while ($row= mysqli_fetch_assoc($result))
	{
		if ($row ['Email']== $Email )
		{
			
			echo"EMAIL EXISTS Try again ?";
			$doesEmailExist == true; 
			return;
			//echo "Password is " . $row;
		}
		else 
		{
			echo "EMAIL iS GOOD";
			$doesEmailExist == false ; 
	    }
	}
}

$sqli = "SELECT  Name FROM login  WHERE Name = '".$username."'";
$result = mysqli_query ($conn , $sqli);

if (mysqli_num_rows ($result) > 0)
{
	// show data for each row
	while ($row= mysqli_fetch_assoc($result))
	{
		if ($row ['Name']== $username )
		{
			
			echo"Name exsits m8 " ; 
			$doesEmailExist == true ; 
			return;
			//echo "Password is " . $row;
		}
		else 
		{
			echo "EMAIL iS GOOD";
			$doesUserExist == false ; 
	    }
	}
}

//$encryptedPass = md5 ($password);
$hashed_password = password_hash($Password, PASSWORD_DEFAULT);
/*while ( $doesUserExist == true || $doesEmailExist = true)
{
    echo"Results not registering as user/email  exist";
	return ; 
}*/

// this string pulls from the SQL database named LOGIN in the format of ID-Name- Email -Password
$sqli = "INSERT INTO login (Name , Email , Password)
          VALUES ('".$username. "','" .$Email."','".$hashed_password. "')";
$result = mysqli_query ($conn , $sqli);
	echo"Results registering";
if ($result==null) 
{
	echo "error";
	return;
}
else 
{
	echo "Success";
}
?>
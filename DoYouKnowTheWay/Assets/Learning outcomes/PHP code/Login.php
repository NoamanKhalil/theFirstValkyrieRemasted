<?php
// script Used to Register/Insert Data

//------------------------------------//
//server login settings 
//echo "Login Running";
$servername = "localhost";
$dbUser= "id4796405_admin";
$dbPassword = "00000";
$dbname = "id4796405_thefirstvalkyrie";
//------------------------------------//

$username =$_POST ["usernamePost"]; // CARLOSISBAE;
$Password=$_POST ["passwordPost"]; // 12345;
//connect to user
$conn = mysqli_connect ($servername,$dbUser,$dbPassword,$dbname);

// check connection 
if (!$conn)
{
	die ("Connection FAILED" . mysqli_error());
}

// this string pulls from the SQL database named LOGIN and comapres passwrod to its user 
$sqli = "SELECT  Password FROM login  WHERE Name = '".$username."'";
          
$result = mysqli_query ($conn , $sqli);

if (mysqli_num_rows ($result) > 0)
{
	// show data for each row
	while ($row= mysqli_fetch_assoc($result))
	{
	    //password_verify($password, $row ['Password']);
		/*if ($row ['Password']== $Password)
		{
			echo "Login Success";
			//echo "Password is " . $row;
		}*/
		$hashed_password= $row ['Password'];
		if (password_verify($Password, $hashed_password))
		{
		   	echo "Login Success";
			//echo "Password is " . $row;
		}
	}
}
else {
	echo "DATA NOT FOUND";
	echo "Pass is " . $row['Password'];
}

?>
<?php

$servername = "localhost";
$dbUser= "id4796405_admin";
$dbPassword = "00000";
$dbname = "id4796405_thefirstvalkyrie";
//------------------------------------//
$username = "123"; // $_POST ["usernamePost"];  
$conn = mysqli_connect ($servername,$dbUser,$dbPassword,$dbname);

// check connection 
if (!$conn)
{
	die ("Connection@@@@FAILED" . mysqli_error());
}
else 
{
	echo "Sucessfull connection";
}

$sql = "UPDATE login SET Games = Games + 1 WHERE Name = '".$username."'";
echo $sql ;

$result = $conn->query($sql);

?>
<?php

$servername = "localhost";
$dbUser= "id4796405_admin";
$dbPassword = "00000";
$dbname = "id4796405_thefirstvalkyrie";
//------------------------------------//
$conn = mysqli_connect ($servername,$dbUser,$dbPassword,$dbname);

// check connection 
if (!$conn)
{
	die ("Connection@@@@FAILED" . mysqli_error());
}
else 
{
	echo "Sucessfull connection /n";
}

$sql = "SELECT * FROM login";
$result = $conn->query($sql);

if($result->num_rows > 0){
    foreach($result as $row)
	{
        echo $row['Name'];
        echo $row['Games'];
    }
}
else
{
    echo "There are no leaderboards at the moment!";
}


?>
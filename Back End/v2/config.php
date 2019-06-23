<?php
// Send variables for the MySQL database class.
    $database = mysqli_connect('68.178.142.2', 'myth7638', 'Raines169!', 'myth7638');
    if(mysqli_connect_errno()){
        die("Failed to connect to MySQL: " . mysqli_connect_error());
    }
//    mysqli_select_db('myth7638', $database) or die('Could not select database');
    $base_url = 'http://www.nickmcgough.com/TableTop/'
?>
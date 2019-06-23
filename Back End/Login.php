<?php
    require_once 'config.php';
	
    $username = $_REQUEST['username'];
    $password = $_REQUEST['password']; 
    $List['username'] = $username;
    $List['password'] = $password;
    $query = "SELECT * FROM Users WHERE username='".mysqli_real_escape_string($database, $username)."'AND password='".$password."'";
    $result=mysqli_query($database, $query) or die('Query failed: ' .mysqli_error($database));

    $num_results = mysqli_num_rows($result); 
	
    if (mysqli_num_rows($result)>0){
        $arr = mysqli_fetch_array($result);
        $List['result'] = "right";
        $List['id'] = $arr['id'];
    } else {
        $List['result'] = "wrong";
        $List['resultNum'] = mysqli_num_rows($result);
    } 
	
    echo json_encode($List);
?>
<?php
    require_once 'config.php';
	
    $username = $_REQUEST['username'];
    $password = $_REQUEST['password']; 

    $query = "insert into Users (username, password) values ('".$username."', '".$password."')";
    $result = mysqli_query($database, $query) or die('Insert Query failed: ' . mysqli_error($database));
	
//    $quid = "SELECT * FROM Login WHERE Username = '".$username."'";
//    $uidresult = mysql_query($quid) or die('Query failed: ' . mysql_error());

//    $uidarr = mysql_fetch_array($uidresult);
    $uid = mysqli_insert_id($database);

    $fillSets = "insert into UserTileSets (UserId, TileSetId) values ('".$uid."', '1'), ('".$uid."', '2'), ('".$uid."', '3'), ('".$uid."', '4'), ('".$uid."', '5')";
    $fillQuery = mysqli_query($database, $fillSets) or die('Sets Query failed: ' . mysqli_error($database));

    $List['result'] = "registered";
    $List['id'] = $uid;
//    $List['result_id'] = $uidresult;

    echo json_encode($List);
?>
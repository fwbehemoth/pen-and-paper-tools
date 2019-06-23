<?php
    require_once 'config.php';
	
    $lid = $_REQUEST['userid'];
    $cid = $_REQUEST['campaignid'];
    $name = $_REQUEST['mapName'];

    $query = "insert into Maps (userid, campaignid, name) values ('".$lid."', '".$cid."', '".$name."');";
    $result = mysqli_query($database, $query) or die('Query failed: ' . mysql_error($database));
	
    $List['result'] = "created";

    echo json_encode($List);
?>
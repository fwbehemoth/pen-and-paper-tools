<?php
	require_once 'config.php';
	
	$cid = $_REQUEST['campaignid'];
        $uid = $_REQUEST['userid'];
	
	$qchar = "SELECT * FROM Campaigns WHERE Id = '".$cid."';";
	$charresult = mysqli_query($database, $qchar) or die('Query failed: ' . mysqli_error($database));
	
	class Item {
		var $Id;
		var $Name;
		var $Game;
		var $Setting;
	}
	
	
	if (mysqli_num_rows($charresult) > 0) {
            $row = mysqli_fetch_row($charresult);
            $item = new Item();
            $item -> Id = $row[0];
            $item -> Name = $row[1];
            $item -> Game = $row[2];
            $item -> Setting = $row[3]; 
        } else {
            echo "no results";
        }
        

	echo json_encode($item);
?>
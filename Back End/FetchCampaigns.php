<?php
	require_once 'config.php';
	
	$lid = $_REQUEST['userid'];
	
	$qchar = "SELECT * FROM Campaigns WHERE UserId = '".$lid."';";
	$charresult = mysqli_query($database, $qchar) or die('Query failed: ' . mysqli_error($database));
	
	class Item {
		var $Id;
		var $Name;
		var $Game;
		var $Setting;
	}
	
	//$arrchar = mysql_fetch_array($charresult);
	
	$cnt = 0;
	while($item = mysqli_fetch_array($charresult, MYSQLI_BOTH)){
		$List[$cnt] = new Item();
		$List[$cnt] -> Id = $item['Id'];
		$List[$cnt] -> Name = $item['Name'];
		$List[$cnt] -> Game = $item['Game'];
		$List[$cnt] -> Setting = $item['Setting'];
		$cnt++;
	}
	
	$List['count'] = $cnt;
	echo json_encode($List);
?>
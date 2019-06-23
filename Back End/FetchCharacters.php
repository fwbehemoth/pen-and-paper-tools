<?php
	require_once 'config.php';
	
	$lid = $_REQUEST['userid'];
	
	$qchar = "SELECT * FROM Characters WHERE UserId = '".$lid."';";
	$charresult = mysqli_query($database, $qchar) or die('Query failed: ' . mysqli_error($database));
	
	class Item {
		var $Id;
		var $Name;
		var $Type;
		var $Race;
		var $Age;
		var $Sex;
		var $Height;
		var $Weight;
                var $CampaignId;
	}
	
	//$arrchar = mysql_fetch_array($charresult);
	
	$cnt = 0;
	while($item = mysqli_fetch_array($charresult, MYSQLI_BOTH)){
		$List[$cnt] = new Item();
		$List[$cnt] -> Id = $item["Id"];
		$List[$cnt] -> Name = $item['Name'];
		$List[$cnt] -> Type = $item['Type'];
		$List[$cnt] -> Race = $item['Race'];
		$List[$cnt] -> Age = $item['Age'];
		$List[$cnt] -> Sex = $item['Sex'];
		$List[$cnt] -> Height = $item['Height'];
		$List[$cnt] -> Weight = $item['Weight'];
                $List[$cnt] -> CampaignId = $item['CampaignId'];
		$cnt++;
	}
	
	$List['count'] = $cnt;
	echo json_encode($List);
?>
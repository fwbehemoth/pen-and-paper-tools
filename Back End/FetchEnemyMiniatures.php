<?php
	require_once 'config.php';
	
	$lid = $_REQUEST['userid'];
	
	//$query = "SELECT * FROM UserTileSets INNER JOIN TileSets ON UserTileSets.TileSetId = TileSets.Id INNER JOIN Tiles ON TileSets.Id = Tiles.TileSetId WHERE UserTileSets.UserId = '".$lid."'";
	$query = "SELECT * FROM UserMiniatureSets INNER JOIN MiniatureSets ON UserMiniatureSets.MiniatureSetId = MiniatureSets.Id WHERE UserMiniatureSets.UserId = '".$lid."'";
	$result = mysqli_query($database, $query) or die('Query failed: ' . mysqli_error($database));
	
	class Set {
		var $Id;
		var $Name;
		var $Miniatures;
                var $MiniaturesCount;
	}
	
	class Miniature {
		var $Id;
		var $Name;
	}
	
	//$arrchar = mysql_fetch_array($charresult);
	
	$setcnt = 0;
	while($item = mysqli_fetch_array($result, MYSQLI_BOTH)){
		$List[$setcnt] = new Set();
		$List[$setcnt] -> Id = $item['Id'];
		$List[$setcnt] -> Name = $item['Name'];
		
		$miniatureq = "SELECT * FROM EnemyMiniatures WHERE MiniatureSetId = '".$item['Id']."'";
		$miniatureres = mysqli_query($database, $tileq) or die('Query failed: ' . mysqli_error($database));
		//$List['Tile'] = mysql_fetch_array($tileres); 
		$miniaturecnt = 0;
		while($item2 = mysqli_fetch_array($tileres,MYSQLI_BOTH)){
			$Sub[$miniaturecnt] = new Miniature();
			$Sub[$miniaturecnt] -> Id = $item2['Id'];
			$Sub[$miniaturecnt] -> Name = $item2['Name'];
			$miniaturecnt++;
		}
		$List[$setcnt] -> MiniaturesCount = $miniaturecnt;
		$List[$setcnt] -> Miniatures = $Sub;
		$setcnt++;
	}
	//$List['userid'] = $lid;
	//$List['array'] = mysql_fetch_array($result);
	$List['count'] = $setcnt;
	echo json_encode($List);
?>
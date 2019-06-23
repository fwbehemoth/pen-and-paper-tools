<?php
	require_once 'config.php';
	
	$lid = $_REQUEST['userid'];
	
	//$query = "SELECT * FROM UserTileSets INNER JOIN TileSets ON UserTileSets.TileSetId = TileSets.Id INNER JOIN Tiles ON TileSets.Id = Tiles.TileSetId WHERE UserTileSets.UserId = '".$lid."'";
	$query = "SELECT * FROM UserTileSets INNER JOIN TileSets ON UserTileSets.TileSetId = TileSets.Id WHERE UserTileSets.UserId = '".$lid."'";
	$result = mysqli_query($database, $query) or die('Query failed: ' . mysqli_error($database));
	
	class Set {
		var $Id;
		var $Name;
		var $Tiles;
                var $TilesCount;
	}
	
	class Tile {
		var $Id;
		var $Name;
		var $Type;
	}
	
	//$arrchar = mysql_fetch_array($charresult);
	
	$setcnt = 0;
	while($item = mysqli_fetch_array($result, MYSQLI_BOTH)){
		$List[$setcnt] = new Set();
		$List[$setcnt] -> Id = $item['Id'];
		$List[$setcnt] -> Name = $item['Name'];
		
		$tileq = "SELECT * FROM Tiles WHERE TileSetId = '".$item['Id']."'";
		$tileres = mysqli_query($database, $tileq) or die('Query failed: ' . mysqli_error($database));
		//$List['Tile'] = mysql_fetch_array($tileres); 
		$tilecnt = 0;
		while($item2 = mysqli_fetch_array($tileres,MYSQLI_BOTH)){
			$Sub[$tilecnt] = new Tile();
			$Sub[$tilecnt] -> Id = $item2['Id'];
			$Sub[$tilecnt] -> Name = $item2['Name'];
			$Sub[$tilecnt] -> Type = $item2['Type'];
			$tilecnt++;
		}
		$List[$setcnt] -> TilesCount = $tilecnt;
		$List[$setcnt] -> Tiles = $Sub;
		$setcnt++;
	}
	//$List['userid'] = $lid;
	//$List['array'] = mysql_fetch_array($result);
	$List['count'] = $setcnt;
	echo json_encode($List);
?>
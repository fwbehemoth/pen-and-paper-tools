<?php
$method = $_SERVER['REQUEST_METHOD'];
$request = explode("/", substr(@$_SERVER['PATH_INFO'], 1));
require_once 'config.php';

$Result['method'] = $method;
$Result['request'] = $data;

function rest_put($Result){
    $Result['function'] = 'put';

    return $Result;
}

function rest_post($Result){
    $Result['function'] = 'post';

    return $Result;
}

function rest_get($Result, $database){
    $Result['function'] = 'get';
    $username = $_REQUEST['username'];
    $password = $_REQUEST['password'];

    $query = "SELECT * FROM Users WHERE username='".mysqli_real_escape_string($database, $username)."'AND password='".$password."'";
    $result=mysqli_query($database, $query) or die('Query failed: ' .mysqli_error($database));

    $num_results = mysqli_num_rows($result);

    if (mysqli_num_rows($result)>0){
        $arr = mysqli_fetch_array($result);
        $Result['result'] = "right";
        $Result['id'] = $arr['id'];
    } else {
        $Result['result'] = "wrong";
        $Result['resultNum'] = mysqli_num_rows($result);
    }

    return $Result;
}

function rest_delete($Result){
    $Result['function'] = 'delete';

    return $Result;
}

function rest_error($Result){
    $Result['function'] = 'error';

    return $Result;
}

switch ($method) {
  case 'PUT':
    $Result = rest_put($Result);
    break;
  case 'POST':
    $Result = rest_post($Result);
    break;
  case 'GET':
    $Result = rest_get($Result, $database);
    break;
  case 'DELETE':
    $Result = rest_delete($Result);
    break;
  default:
    $Result = rest_error($Result);
    break;
}

echo json_encode($Result);
?>
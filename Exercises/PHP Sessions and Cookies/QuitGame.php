<?php

session_start();
$_SESSION = [];
$params = session_get_cookie_params();
// expire the session cookie
setcookie(session_name(), "", time()-42000, $params['path'], $params['domain'], $params['secure'], $params['httponly']);
// expire the user cookie
setcookie('user', '', time() - 42000);
// expire the bgColor cookie
if( isset($_COOKIE['bgColor']) ){
  setcookie('bgColor', '', time()-42000);
}

session_destroy();
header("Location: Start.php");
die();
?>
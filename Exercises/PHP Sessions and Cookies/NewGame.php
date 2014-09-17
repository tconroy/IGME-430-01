<?php
session_start();

if( $_SERVER['REQUEST_METHOD'] == 'POST' ){

  if( ! isset($_SESSION['tacos']) ){
    $username = $_POST['user'];
    setcookie("user", $username);
    $_SESSION['tacos']       = 1;
    $_SESSION['tacos_eaten'] = 0;
    $_SESSION['last_access'] = time();
  }

  header("Location: Game.php");
  die();

}else{
  if (! isset($_SESSION['tacos']) ) {
    header("Location: Start.php");
  }else{
    header("Location: Game.php");
  }
  die();
}

?>
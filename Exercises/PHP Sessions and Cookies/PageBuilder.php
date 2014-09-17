<?php

function addHTMLHeader() {
  $header = file_get_contents('header.html');
  return $header;
}

function addHTMLFooter() {
  $footer = file_get_contents('footer.html');
  return $footer;
}

?>
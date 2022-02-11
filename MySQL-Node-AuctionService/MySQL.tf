provider "azurerm" {
  features {}
}
resource "azurerm_mysql_server" "resource-values" {
  name                         = "mysql-oas-dev"
  location                     = "East US"
  resource_group_name          = "cloud-shell-storage-eastus"
  administrator_login          = "mysqladmin"
  administrator_login_password = "P@ssw0rd!@#"
  sku_name                     = "B_Gen5_2"
  storage_mb                   = 5120
  version                      = "5.7"
  auto_grow_enabled            = true
  backup_retention_days        = 7
  ssl_enforcement_enabled      = false
}


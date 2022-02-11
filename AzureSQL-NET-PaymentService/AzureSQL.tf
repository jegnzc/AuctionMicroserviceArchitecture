provider "azurerm" {
  features {}
}
resource "azurerm_sql_server" "oasresource" {
  name                         = "oas-sqls-dev"
  resource_group_name          = "cloud-shell-storage-eastus"
  location                     = "East US"
  version                      = "12.0"
  administrator_login          = "sqladmin"
  administrator_login_password = "P@ssw0rd!@#"
}

resource "azurerm_sql_database" "oasresource" {
  server_name              = "oas-sqls-dev"
  name                = "auctionpaymentdb"
  resource_group_name = "cloud-shell-storage-eastus"
  location            = "East US"
}

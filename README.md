# Zadatak 4

## ASP .NET CORE WEB API 5.0

### Napisati API upotrebom standardne biblioteke ili u frameworku po izboru koji ima sledece mogucnosti:
    * upload ruta
        * prihvata input JSON
        * definisan je strukturom
            type Input struct{
                FirstName string
                LastName  string
                Telephone string
            }
        * snima JSON na disk
        * validiraj ulaz, ne dozvoli da unese prazna polja
        * ako rekord vec postoji, odbij unos

    * list ruta, prikazuje snimljene telefone koristeci sledecu strukturu:
        type Output struct {
            Results []Input
        }

    * get ruta, prikazuje jedan rekord po ID-u
    * delete ruta, brise jedan rekord po ID-u

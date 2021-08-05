export const ApplicationName = 'BookshopApp';

const accounttPrefix = "/Account"
const productPrefix = "/Product"
const orderPrefix = "/Order"

const apiPrefix = "/api"
const apiAccountPrefix = `${apiPrefix}${accounttPrefix}`
const apiProductPrefix = `${apiPrefix}${productPrefix}`
const apiOrderPrefix = `${apiPrefix}${orderPrefix}`

export const AppPagePaths = {

    Product:                `${productPrefix}`,
    ProductManipulator:     `${productPrefix}/ProductManipulator`,
    Cart:                   `${orderPrefix}/Cart`,
    Orders:                 `${orderPrefix}/Orders`,
    Order:                  `${orderPrefix}`,

    Login:                  `${accounttPrefix}/Login`,
    Register:               `${accounttPrefix}/Signup`,
    Profile:                `${accounttPrefix}/Profile`,
    Logout:                 `${accounttPrefix}/Logout`,

}

export const AppApiPaths = {
    Cart:                   `${apiOrderPrefix}/Cart`,
    CartedProductCancel:    `${apiOrderPrefix}/Cart/Cancel`,
    PlaceAnOrder:           `${apiOrderPrefix}/Cart/PlaceAnOrder`,
    AddToCart:              `${apiOrderPrefix}/AddToCart`,
    Orders:                 `${apiOrderPrefix}/Orders`,
    Order:                  `${apiOrderPrefix}`,
    GlobalOrders:           `${apiOrderPrefix}/GlobalOrders`,

    Product:                `${apiProductPrefix}`,
    Products:               `${apiProductPrefix}/Prods`,
    ProductManipulator:     `${apiProductPrefix}/ProductManipulator`,

    Login:                  `${apiAccountPrefix}/Login`,
    Register:               `${apiAccountPrefix}/Signup`,
    Profile:                `${apiAccountPrefix}/Profile`,
    Logout:                 `${apiAccountPrefix}/Logout`,
    Permission:             `${apiAccountPrefix}/Permission`,
    IsUserAuthenticated:    `${apiAccountPrefix}/Check`,
};

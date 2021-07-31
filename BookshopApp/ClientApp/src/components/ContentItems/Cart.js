import React, { Component, Fragment } from 'react';
import { Pagination } from './SubComponents/Pagination';
import { AppApiPaths } from '../Api-authorization/AppConstants';
import { ProductCard } from './SubComponents/ProductCard';
import { CartedCard } from './SubComponents/CartedCard';

export class Cart extends Component {
    constructor(props) {
        super(props);

        this.state = {
            page: 1,
            cart: null,
            pageIsLast: false,
            dataWasUpdated: true,
        }

    }

    componentDidMount() {
        this.getProductsFromServer();
    }

    componentDidUpdate() {
        if (!this.state.dataWasUpdated)
            this.getProductsFromServer();
    }

    async getProductsFromServer() {
        //The server pagination starts from zero
        let response = await fetch(AppApiPaths.Cart + '/' + (this.state.page - 1), {
            headers: {
                'Content-Type': 'application/json',
                'Accept': 'application/json'
            }
        });

        if (!response.ok)
            console.log("error");
        else {
            const answer = await response.json();
            this.setState({ cart: answer.cart, pageIsLast: answer.pageIsLast, dataWasUpdated: true });
        }
    }

    render() {
        const cart = this.state.cart;
        if (cart == null)
            return (<div />);

        return (
            <Fragment>
                <div className="row mt-3 ">
                    <h3 className="col-md ">Продукты в корзине</h3>
                    <button type="button" className="col-md ">Оформить заказ</button>
                </div>
                { cart.orderedProducts.map((prod, i) => <CartedCard key={i} product={prod.product} countOfDeferredProduct={prod.count}/>)}
                <Pagination curPage={this.state.page} pageIsLast={this.state.pageIsLast} changePage={(newPage) => this.setState({ page: newPage, dataWasUpdated: false })} />
            </Fragment>);
    }
}
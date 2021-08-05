import React, { Component, Fragment } from 'react';
import { Pagination } from '../SubComponents/Pagination';
import { AppApiPaths } from '../Api-authorization/AppConstants';
import { OrderedProductCard } from '../SubComponents/OrderedProductCard';

export class Cart extends Component {
    constructor(props) {
        super(props);

        this.state = {
            page: 1,
            cart: null,
            pageIsLast: false,
            dataWasUpdated: true,
            discountPercent: 0,
        }

        this.handlePlaceAnOrder = this.handlePlaceAnOrder.bind(this);
    }

    componentDidMount() {
        this.getProductsFromServer();
    }

    componentDidUpdate() {
        if (!this.state.dataWasUpdated)
            this.getProductsFromServer();
    }

    async handlePlaceAnOrder() {

        if (this.state.cart.orderedProducts.length === 0) {
            return alert("В корзине нет ни одного продукта");
        }

        var response = await fetch(AppApiPaths.PlaceAnOrder, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json;charset=utf-8'
            }
        });

        if (!response.ok) {
            console.log("error")
        }
        else {
            alert("Заказ был успешно оформлен");
            this.setState({ dataWasUpdated: false });
        }
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
            this.setState({ cart: answer.cart, pageIsLast: answer.pageIsLast, discountPercent: answer.discountPercent, dataWasUpdated: true });
        }
    }

    render() {
        const cart = this.state.cart;
        if (cart === null)
            return (<div />);

        if (cart.orderedProducts.length === 0)
            return (
                <div className="row mt-3 ">
                    <div className="col">
                        <h3>Ваша корзина пуста</h3>
                    </div>
                </div>);

        return (
            <Fragment>
                <div className="row mt-3 ">
                    <div className="col">
                        <div className="row no-gutters">
                            <button type="button" className="btn btn-primary btn-block" onClick={this.handlePlaceAnOrder}>Оформить заказ</button>
                        </div>
                        <div className="row no-gutters mt-3">
                            <h3>Продуктов в корзине на {cart.finalAmount}$</h3>
                            {this.state.discountPercent !== 0
                                ? <h4>Скидка по дисконтной карте: {cart.finalAmount / 100 * this.state.discountPercent}$</h4>
                                : <h4>У вас нет скидки по дисконтной карте</h4>}
                        </div>
                    </div>
                </div>
                {cart.orderedProducts.map((prod, i) => <OrderedProductCard key={i} product={prod.product} countOfDeferredProduct={prod.count} updateCart={() => this.setState({ dataWasUpdated: false })} />)}
                <Pagination curPage={this.state.page} pageIsLast={this.state.pageIsLast} changePage={(newPage) => this.setState({ page: newPage, dataWasUpdated: false })} />
            </Fragment>);
    }
}
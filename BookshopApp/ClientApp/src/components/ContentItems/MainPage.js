import React, { Component, Fragment } from 'react';
import { Searcher } from '../SubComponents/Searcher';
import { ProductCard } from '../SubComponents/ProductCard';
import { Pagination } from '../SubComponents/Pagination';
import { AppApiPaths } from '../Api-authorization/AppConstants';

export class MainPage extends Component {
    constructor(props) {
        super(props);

        this.state = {
            page: 1,
            products: [],
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
        let response = await fetch(AppApiPaths.Products + '/' + (this.state.page - 1), {
            headers: {
                'Content-Type': 'application/json',
                'Accept': 'application/json'
            }
        });

        if (!response.ok)
            console.log("error");
        else
        {
            const answer = await response.json();
            this.setState({ products: answer.prods, pageIsLast: answer.pageIsLast, dataWasUpdated: true });
        }
    }

    render() {
        const prods = this.state.products;

        return (
            <Fragment>
                <Searcher />
                {prods.map((prod, i) => <ProductCard key={i} product={prod} />)}
                <Pagination curPage={this.state.page} pageIsLast={this.state.pageIsLast} changePage={(newPage) => this.setState({ page: newPage, dataWasUpdated: false })} />
            </Fragment>
        );
    }
}

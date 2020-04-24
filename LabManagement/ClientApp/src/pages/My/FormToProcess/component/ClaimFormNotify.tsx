import React, { FC, useRef, useState, useEffect } from 'react';
import {
    Card,
    Input,
    List,
    Radio,
} from 'antd';
import styles from './style.less'
import { IChemical, IClaimForm } from '@/models/entity';

const ListContent = ({
    data: { state },
}: {
    data: IChemical;
}) => (
        <div className={styles.listContent}>
            <div className={styles.listContentItem}>
                <span>表单状态</span>
                <p>{state}</p>
            </div>
        </div>
    );

export const ClaimFormList = (param: {
    cform?: IClaimForm[]
}) => {
    const { cform } = param;

    const paginationProps = {
        showSizeChanger: true,
        showQuickJumper: true,
        pageSize: 5,
        total: cform?.length || 0,
    };

    return (
        <div className={styles.standardList}>
            <List
                size="large"
                rowKey="id"
                pagination={paginationProps}
                dataSource={cform}
                renderItem={(item) => (
                    <List.Item
                        actions={[
                            <a
                                key="edit"
                                onClick={(e) => {
                                    
                                }}
                            >
                                查看该次申请的化学危险品
                            </a>,
                        ]}
                    >
                        <List.Item.Meta
                            title={<span>{'申请表编号：'+item.id}</span>}
                            description={<div>
                                <span>所属实验室：</span>
                                <span>{item.lid}</span>
                            </div>}
                        />
                        <ListContent data={item} />
                    </List.Item>
                )}
            />
        </div>
    );
};
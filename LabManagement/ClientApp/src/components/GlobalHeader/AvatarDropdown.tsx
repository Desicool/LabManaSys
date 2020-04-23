import { LogoutOutlined, SettingOutlined, UserOutlined, MessageOutlined } from '@ant-design/icons';
import { Menu, Spin } from 'antd';
import { ClickParam } from 'antd/es/menu';
import React from 'react';
import { history, ConnectProps, connect } from 'umi';
import { ConnectState } from '@/models/connect';
import { IUser } from '@/models/user';
import HeaderDropdown from '../HeaderDropdown';
import styles from './index.less';

export interface GlobalHeaderRightProps extends Partial<ConnectProps> {
  currentUser?: IUser;
  menu?: boolean;
}

const AvatarDropdown: React.FC<GlobalHeaderRightProps> = (props) => {
  const { dispatch } = props;
  const {
    currentUser = {
      userName: '',
    },
  } = props;
  const menuHeaderDropdown = (
    <Menu className={styles.menu}>
      <Menu.Item key="message" onClick={() => {
        history.push('/my/process');
      }}>
        <MessageOutlined />
          我的待办
        </Menu.Item>
      <Menu.Divider />

      <Menu.Item key="logout" onClick={() => {
        if (dispatch) {
          dispatch({
            type: 'login/logout',
          });
        }
      }}>
        <LogoutOutlined />
          退出登录
        </Menu.Item>
    </Menu>
  );
  return currentUser && currentUser.userName ? (
    <HeaderDropdown overlay={menuHeaderDropdown}>
      <span className={`${styles.action} ${styles.account}`}>
        <span className={styles.name}>{currentUser.userName}</span>
      </span>
    </HeaderDropdown>
  ) : (
      <span className={`${styles.action} ${styles.account}`}>
        <Spin
          size="small"
          style={{
            marginLeft: 8,
            marginRight: 8,
          }}
        />
      </span>
    );
}

export default connect(({ user }: ConnectState) => ({
  currentUser: user.currentUser,
}))(AvatarDropdown);
